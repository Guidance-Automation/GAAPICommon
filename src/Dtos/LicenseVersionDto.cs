using ProtoBuf;

namespace GAAPICommon.Core.Dtos;

[ProtoContract]
public class LicenseVersionDto
{
    [ProtoMember(1)]
    public int MajorVersion { get; set; }

    [ProtoMember(2)]
    public int MinorVersion { get; set; }
}