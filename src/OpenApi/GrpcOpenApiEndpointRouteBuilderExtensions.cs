using Google.Api;
using Google.Protobuf.Reflection;
using Grpc.AspNetCore.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace GAAPICommon.OpenApi;

/// <summary>
/// Options for an OpenAPI document generated from the mapped GAAPI gRPC services.
/// </summary>
public sealed class GrpcOpenApiOptions
{
    /// <summary>The document title displayed by an OpenAPI client such as Scalar.</summary>
    public string Title { get; set; } = "Guidance gRPC API";

    /// <summary>The documented API version.</summary>
    public string Version { get; set; } = "v1";

    /// <summary>An optional description of the API.</summary>
    public string? Description { get; set; }

    /// <summary>
    /// Optional protobuf service full names to include. When empty, every mapped
    /// GAAPI gRPC service is included.
    /// </summary>
    public ISet<string> IncludedServices { get; } = new HashSet<string>(StringComparer.Ordinal);
}

/// <summary>Maps OpenAPI documents generated directly from protobuf descriptors.</summary>
public static partial class GrpcOpenApiEndpointRouteBuilderExtensions
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true
    };

    /// <summary>
    /// Maps an OpenAPI 3.1 document for the JSON-transcoded methods belonging to
    /// the GAAPI gRPC services mapped on this endpoint route builder.
    /// </summary>
    public static RouteHandlerBuilder MapGrpcOpenApiDocument(
        this IEndpointRouteBuilder endpoints,
        string pattern,
        Action<GrpcOpenApiOptions>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(endpoints);
        ArgumentException.ThrowIfNullOrWhiteSpace(pattern);

        GrpcOpenApiOptions options = new();
        configure?.Invoke(options);

        return endpoints.MapGet(pattern, () =>
        {
            HashSet<string> mappedServiceNames = endpoints.DataSources
                .SelectMany(dataSource => dataSource.Endpoints)
                .Select(endpoint => endpoint.Metadata.GetMetadata<GrpcMethodMetadata>()?.Method.ServiceName)
                .Where(serviceName => !string.IsNullOrWhiteSpace(serviceName))
                .Cast<string>()
                .ToHashSet(StringComparer.Ordinal);

            Dictionary<string, object?> document = GrpcOpenApiDocumentGenerator.Generate(
                options,
                mappedServiceNames);
            return Results.Json(document, SerializerOptions);
        })
        .ExcludeFromDescription();
    }

    private static partial class GrpcOpenApiDocumentGenerator
    {
        private static readonly IReadOnlyDictionary<string, ServiceDescriptor> Descriptors =
            LoadServiceDescriptors();

        [GeneratedRegex("\\{(?<field>[A-Za-z0-9_.]+)(?:=[^}]*)?\\}")]
        private static partial Regex PathParameterExpression();

        [GeneratedRegex("(?<!^)([A-Z])")]
        private static partial Regex PascalCaseBoundaryExpression();

        public static Dictionary<string, object?> Generate(
            GrpcOpenApiOptions options,
            IReadOnlySet<string> mappedServiceNames)
        {
            SortedDictionary<string, object?> paths = new(StringComparer.Ordinal);
            SortedDictionary<string, object?> schemas = new(StringComparer.Ordinal);
            List<object?> tags = [];

            IEnumerable<ServiceDescriptor> services = Descriptors.Values
                .Where(descriptor => mappedServiceNames.Contains(descriptor.FullName))
                .Where(descriptor => options.IncludedServices.Count == 0 ||
                    options.IncludedServices.Contains(descriptor.FullName))
                .OrderBy(descriptor => descriptor.FullName, StringComparer.Ordinal);

            foreach (ServiceDescriptor service in services)
            {
                bool addedService = false;
                foreach (MethodDescriptor method in service.Methods)
                {
                    HttpRule? rule = method.GetOptions().GetExtension(AnnotationsExtensions.Http);
                    if (rule == null)
                        continue;

                    foreach (HttpRule binding in EnumerateBindings(rule))
                    {
                        if (!TryGetHttpBinding(binding, out string httpMethod, out string route))
                            continue;

                        AddOperation(paths, schemas, service, method, binding, httpMethod, route);
                        addedService = true;
                    }
                }

                if (addedService)
                {
                    Dictionary<string, object?> tag = new()
                    {
                        ["name"] = service.Name
                    };
                    AddDescription(tag, service.Declaration?.LeadingComments);
                    tags.Add(tag);
                }
            }

            Dictionary<string, object?> info = new()
            {
                ["title"] = options.Title,
                ["version"] = options.Version
            };
            AddDescription(info, options.Description);

            return new Dictionary<string, object?>
            {
                ["openapi"] = "3.1.0",
                ["info"] = info,
                ["tags"] = tags,
                ["paths"] = paths,
                ["components"] = new Dictionary<string, object?>
                {
                    ["schemas"] = schemas
                }
            };
        }

        private static IReadOnlyDictionary<string, ServiceDescriptor> LoadServiceDescriptors()
        {
            return typeof(GrpcOpenApiEndpointRouteBuilderExtensions).Assembly
                .GetTypes()
                .Select(type => type.GetProperty(
                    "Descriptor",
                    BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
                .Where(property => property?.PropertyType == typeof(ServiceDescriptor))
                .Select(property => property!.GetValue(null))
                .OfType<ServiceDescriptor>()
                .GroupBy(descriptor => descriptor.FullName, StringComparer.Ordinal)
                .ToDictionary(group => group.Key, group => group.First(), StringComparer.Ordinal);
        }

        private static IEnumerable<HttpRule> EnumerateBindings(HttpRule rule)
        {
            yield return rule;
            foreach (HttpRule additionalBinding in rule.AdditionalBindings)
            {
                foreach (HttpRule nestedBinding in EnumerateBindings(additionalBinding))
                    yield return nestedBinding;
            }
        }

        private static bool TryGetHttpBinding(
            HttpRule rule,
            out string httpMethod,
            out string route)
        {
            (httpMethod, route) = rule.PatternCase switch
            {
                HttpRule.PatternOneofCase.Get => ("get", rule.Get),
                HttpRule.PatternOneofCase.Put => ("put", rule.Put),
                HttpRule.PatternOneofCase.Post => ("post", rule.Post),
                HttpRule.PatternOneofCase.Delete => ("delete", rule.Delete),
                HttpRule.PatternOneofCase.Patch => ("patch", rule.Patch),
                HttpRule.PatternOneofCase.Custom =>
                    (rule.Custom.Kind.ToLowerInvariant(), rule.Custom.Path),
                _ => (string.Empty, string.Empty)
            };

            return !string.IsNullOrWhiteSpace(httpMethod) && !string.IsNullOrWhiteSpace(route);
        }

        private static void AddOperation(
            SortedDictionary<string, object?> paths,
            SortedDictionary<string, object?> schemas,
            ServiceDescriptor service,
            MethodDescriptor method,
            HttpRule rule,
            string httpMethod,
            string route)
        {
            MatchCollection routeMatches = PathParameterExpression().Matches(route);
            string openApiRoute = PathParameterExpression().Replace(
                route.StartsWith('/') ? route : $"/{route}",
                match => $"{{{match.Groups["field"].Value}}}");

            if (!paths.TryGetValue(openApiRoute, out object? pathValue))
            {
                pathValue = new SortedDictionary<string, object?>(StringComparer.Ordinal);
                paths[openApiRoute] = pathValue;
            }

            SortedDictionary<string, object?> path =
                (SortedDictionary<string, object?>)pathValue!;
            if (path.ContainsKey(httpMethod))
                return;

            HashSet<string> pathFields = routeMatches
                .Select(match => match.Groups["field"].Value.Split('.')[0])
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            Dictionary<string, object?> operation = new()
            {
                ["operationId"] = $"{service.Name}_{method.Name}",
                ["summary"] = Summary(method),
                ["tags"] = new[] { service.Name },
                ["parameters"] = CreateParameters(method.InputType, rule, pathFields, schemas),
                ["responses"] = CreateResponses(method, schemas),
                ["x-grpc-service"] = service.FullName,
                ["x-grpc-method"] = method.Name
            };
            AddDescription(operation, method.Declaration?.LeadingComments);

            Dictionary<string, object?>? requestBody = CreateRequestBody(method.InputType, rule, schemas);
            if (requestBody != null)
                operation["requestBody"] = requestBody;
            if (method.IsClientStreaming)
                operation["x-grpc-client-streaming"] = true;
            if (method.IsServerStreaming)
                operation["x-grpc-server-streaming"] = true;

            path[httpMethod] = operation;
        }

        private static List<object?> CreateParameters(
            MessageDescriptor request,
            HttpRule rule,
            IReadOnlySet<string> pathFields,
            SortedDictionary<string, object?> schemas)
        {
            List<object?> parameters = [];
            foreach (string pathFieldName in pathFields)
            {
                FieldDescriptor? field = FindField(request, pathFieldName);
                Dictionary<string, object?> parameter = new()
                {
                    ["name"] = pathFieldName,
                    ["in"] = "path",
                    ["required"] = true,
                    ["schema"] = field == null
                        ? new Dictionary<string, object?> { ["type"] = "string" }
                        : SchemaForField(field, schemas)
                };
                AddDescription(parameter, field?.Declaration?.LeadingComments);
                parameters.Add(parameter);
            }

            string bodyField = rule.Body.Split('.')[0];
            foreach (FieldDescriptor field in request.Fields.InDeclarationOrder())
            {
                if (pathFields.Contains(field.Name) || pathFields.Contains(field.JsonName) ||
                    rule.Body == "*" ||
                    (!string.IsNullOrWhiteSpace(bodyField) &&
                        (bodyField.Equals(field.Name, StringComparison.OrdinalIgnoreCase) ||
                         bodyField.Equals(field.JsonName, StringComparison.OrdinalIgnoreCase))))
                {
                    continue;
                }

                Dictionary<string, object?> parameter = new()
                {
                    ["name"] = field.JsonName,
                    ["in"] = "query",
                    ["required"] = false,
                    ["schema"] = SchemaForField(field, schemas)
                };
                AddDescription(parameter, field.Declaration?.LeadingComments);
                parameters.Add(parameter);
            }

            return parameters;
        }

        private static Dictionary<string, object?>? CreateRequestBody(
            MessageDescriptor request,
            HttpRule rule,
            SortedDictionary<string, object?> schemas)
        {
            if (string.IsNullOrWhiteSpace(rule.Body))
                return null;

            Dictionary<string, object?> schema;
            if (rule.Body == "*")
            {
                schema = Reference(request, schemas);
            }
            else
            {
                FieldDescriptor? bodyField = FindField(request, rule.Body.Split('.')[0]);
                if (bodyField == null)
                    return null;
                schema = SchemaForField(bodyField, schemas);
            }

            return new Dictionary<string, object?>
            {
                ["required"] = true,
                ["content"] = new Dictionary<string, object?>
                {
                    ["application/json"] = new Dictionary<string, object?>
                    {
                        ["schema"] = schema
                    }
                }
            };
        }

        private static Dictionary<string, object?> CreateResponses(
            MethodDescriptor method,
            SortedDictionary<string, object?> schemas)
        {
            Dictionary<string, object?> responseSchema = Reference(method.OutputType, schemas);
            if (method.IsServerStreaming)
            {
                responseSchema = new Dictionary<string, object?>
                {
                    ["type"] = "array",
                    ["items"] = responseSchema
                };
            }

            Dictionary<string, object?> response = new()
            {
                ["description"] = Description(method.OutputType) ?? "Successful response",
                ["content"] = new Dictionary<string, object?>
                {
                    ["application/json"] = new Dictionary<string, object?>
                    {
                        ["schema"] = responseSchema
                    }
                }
            };

            return new Dictionary<string, object?>
            {
                ["200"] = response
            };
        }

        private static Dictionary<string, object?> SchemaForField(
            FieldDescriptor field,
            SortedDictionary<string, object?> schemas)
        {
            if (field.IsMap)
            {
                FieldDescriptor valueField = field.MessageType.FindFieldByNumber(2);
                return new Dictionary<string, object?>
                {
                    ["type"] = "object",
                    ["additionalProperties"] = SchemaForSingularField(valueField, schemas)
                };
            }

            Dictionary<string, object?> schema = SchemaForSingularField(field, schemas);
            if (!field.IsRepeated)
                return schema;

            return new Dictionary<string, object?>
            {
                ["type"] = "array",
                ["items"] = schema
            };
        }

        private static Dictionary<string, object?> SchemaForSingularField(
            FieldDescriptor field,
            SortedDictionary<string, object?> schemas)
        {
            return field.FieldType switch
            {
                FieldType.Bool => TypeSchema("boolean"),
                FieldType.Bytes => TypeSchema("string", "byte"),
                FieldType.Double => TypeSchema("number", "double"),
                FieldType.Float => TypeSchema("number", "float"),
                FieldType.Int32 or FieldType.SInt32 or FieldType.SFixed32 =>
                    TypeSchema("integer", "int32"),
                FieldType.UInt32 or FieldType.Fixed32 =>
                    TypeSchema("integer", "uint32", minimum: 0),
                FieldType.Int64 or FieldType.SInt64 or FieldType.SFixed64 =>
                    TypeSchema("string", "int64"),
                FieldType.UInt64 or FieldType.Fixed64 =>
                    TypeSchema("string", "uint64"),
                FieldType.String => TypeSchema("string"),
                FieldType.Enum => new Dictionary<string, object?>
                {
                    ["type"] = "string",
                    ["enum"] = field.EnumType.Values.Select(value => value.Name).ToArray()
                },
                FieldType.Message or FieldType.Group => Reference(field.MessageType, schemas),
                _ => TypeSchema("string")
            };
        }

        private static Dictionary<string, object?> Reference(
            MessageDescriptor descriptor,
            SortedDictionary<string, object?> schemas)
        {
            string? wellKnownType = descriptor.FullName switch
            {
                "google.protobuf.Timestamp" => "date-time",
                "google.protobuf.Duration" => "duration",
                "google.protobuf.FieldMask" => "field-mask",
                _ => null
            };
            if (wellKnownType != null)
                return TypeSchema("string", wellKnownType);

            string schemaName = SchemaName(descriptor);
            EnsureSchema(descriptor, schemaName, schemas);
            return new Dictionary<string, object?>
            {
                ["$ref"] = $"#/components/schemas/{schemaName}"
            };
        }

        private static void EnsureSchema(
            MessageDescriptor descriptor,
            string schemaName,
            SortedDictionary<string, object?> schemas)
        {
            if (schemas.ContainsKey(schemaName))
                return;

            Dictionary<string, object?> schema = new()
            {
                ["type"] = "object"
            };
            AddDescription(schema, descriptor.Declaration?.LeadingComments);
            schemas[schemaName] = schema;

            SortedDictionary<string, object?> properties = new(StringComparer.Ordinal);
            foreach (FieldDescriptor field in descriptor.Fields.InDeclarationOrder())
            {
                Dictionary<string, object?> fieldSchema = SchemaForField(field, schemas);
                AddDescription(fieldSchema, field.Declaration?.LeadingComments);
                properties[field.JsonName] = fieldSchema;
            }

            schema["properties"] = properties;
        }

        private static Dictionary<string, object?> TypeSchema(
            string type,
            string? format = null,
            int? minimum = null)
        {
            Dictionary<string, object?> schema = new()
            {
                ["type"] = type
            };
            if (!string.IsNullOrWhiteSpace(format))
                schema["format"] = format;
            if (minimum.HasValue)
                schema["minimum"] = minimum.Value;
            return schema;
        }

        private static FieldDescriptor? FindField(MessageDescriptor message, string name)
        {
            return message.Fields.InDeclarationOrder().FirstOrDefault(field =>
                field.Name.Equals(name, StringComparison.OrdinalIgnoreCase) ||
                field.JsonName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        private static string SchemaName(MessageDescriptor descriptor)
        {
            return descriptor.FullName.Replace('.', '_');
        }

        private static string Summary(MethodDescriptor method)
        {
            string? description = Description(method);
            if (!string.IsNullOrWhiteSpace(description))
                return description.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries)[0];

            return PascalCaseBoundaryExpression().Replace(method.Name, " $1");
        }

        private static string? Description(DescriptorBase descriptor)
        {
            return NormaliseDescription(descriptor.Declaration?.LeadingComments);
        }

        private static void AddDescription(Dictionary<string, object?> target, string? description)
        {
            string? normalised = NormaliseDescription(description);
            if (!string.IsNullOrWhiteSpace(normalised))
                target["description"] = normalised;
        }

        private static string? NormaliseDescription(string? description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return null;

            return string.Join(
                Environment.NewLine,
                description.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries)
                    .Select(line => line.Trim()));
        }
    }
}
