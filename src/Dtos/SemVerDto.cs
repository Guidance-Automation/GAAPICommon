using ProtoBuf;

namespace GAAPICommon.Core.Dtos;

/// <summary>
/// This goes out on gRPC so needs ProtoContract and ProtoMember attributes.
/// </summary>
[ProtoContract]
public class SemVerDto
{
    [ProtoMember(1)]
    public int Major { get; set; } = -1;

    [ProtoMember(2)]
    public int Minor { get; set; } = -1;

    [ProtoMember(3)]
    public int Patch { get; set; } = -1;

    [ProtoMember(4)]
    public string ReleaseFlag { get; set; } = string.Empty;
}