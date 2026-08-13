using GAAPICommon.OpenApi;
using GAAPICommon.Services.Maps;
using Google.Api;
using ProtobufServiceDescriptor = Google.Protobuf.Reflection.ServiceDescriptor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Text.Json;

namespace GAAPICommon.Tests;

[TestFixture]
public sealed class GrpcOpenApiDocumentTests
{
    [Test]
    public void EveryHttpRepresentableRpcHasADocumentationBinding()
    {
        ProtobufServiceDescriptor[] services =
        [
            GAAPICommon.Services.Agents.AgentServiceProto.Descriptor,
            GAAPICommon.Services.FleetManager.FleetManagerServiceProto.Descriptor,
            GAAPICommon.Services.Jobs.JobBuilderServiceProto.Descriptor,
            GAAPICommon.Services.Jobs.JobsStateServiceProto.Descriptor,
            GAAPICommon.Services.Jobs.JobStateServiceProto.Descriptor,
            GAAPICommon.Services.Jobs.TaskStateServiceProto.Descriptor,
            MapServiceProto.Descriptor,
            GAAPICommon.Services.Scheduling.SchedulingServiceProto.Descriptor,
            GAAPICommon.Services.Servicing.ServicingServiceProto.Descriptor
        ];

        string[] undocumentedMethods = [.. services
            .SelectMany(service => service.Methods)
            .Where(method => !method.IsClientStreaming)
            .Where(method => !method.GetOptions().HasExtension(AnnotationsExtensions.Http))
            .Select(method => method.FullName)];

        Assert.That(
            undocumentedMethods,
            Is.Empty,
            "Every RPC supported by JSON transcoding must declare a google.api.http binding.");
    }

    [Test]
    public async Task MapServiceDocumentIncludesOperationsAndSchemas()
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddGrpc().AddJsonTranscoding();

        await using WebApplication app = builder.Build();
        app.MapGrpcService<TestMapService>();
        app.MapGrpcOpenApiDocument("/openapi/maps.json");

        await app.StartAsync();
        using HttpClient client = app.GetTestClient();
        using JsonDocument document = JsonDocument.Parse(
            await client.GetStringAsync("/openapi/maps.json"));

        JsonElement root = document.RootElement;
        Assert.Multiple(() =>
        {
            Assert.That(
                root.GetProperty("paths").TryGetProperty("/Map/GetActiveRoadmap", out _),
                Is.True);
            Assert.That(
                root.GetProperty("paths").TryGetProperty(
                    "/Map/UpdateRoadmapMetadata/{MapId}",
                    out JsonElement updateMetadataPath)
                && updateMetadataPath.TryGetProperty("put", out _),
                Is.True);
            Assert.That(
                root.GetProperty("components")
                    .GetProperty("schemas")
                    .EnumerateObject()
                    .Count(),
                Is.GreaterThan(0));
        });
    }

    private sealed class TestMapService : MapServiceProto.MapServiceProtoBase;
}
