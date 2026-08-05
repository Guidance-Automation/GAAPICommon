using GAAPICommon.OpenApi;
using GAAPICommon.Services.Maps;
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
                root.GetProperty("components")
                    .GetProperty("schemas")
                    .EnumerateObject()
                    .Count(),
                Is.GreaterThan(0));
        });
    }

    private sealed class TestMapService : MapServiceProto.MapServiceProtoBase;
}
