using System.Net;
using System.Text.Json.Serialization;

namespace GAAPICommon.Core.Test;

internal class FooIPAddressable
{
    public string Alias { get; set; } = "Alias";

    public int Id { get; set; } = -1;

    [JsonConverter(typeof(IPAddressJsonConverter))]
    public IPAddress? IPAddress { get; set; }
}