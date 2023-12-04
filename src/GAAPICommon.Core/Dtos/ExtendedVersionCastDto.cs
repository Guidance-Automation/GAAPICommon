using System.Net;
using System.Text.Json.Serialization;

namespace GAAPICommon.Core.Dtos;

public class ExtendedVersionCastDto
{
    public byte[]? DynamicData { get; set; }

    [JsonConverter(typeof(IPAddressJsonConverter))]
    public IPAddress? IPAddress { get; set; } 

    public ushort Port { get; set; } = 0;

    public SemVerDto? SemVer { get; set; }
}