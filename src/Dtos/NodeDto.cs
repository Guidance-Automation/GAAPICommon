using GAAPICommon.Architecture;
using ProtoBuf;
using System.Collections.Generic;

namespace GAAPICommon.Core.Dtos;

[ProtoContract]
public class NodeDto
{
    [ProtoMember(1)]
    public int Id { get; set; } = -1;

    [ProtoMember(2)]
    public string Alias { get; set; } = string.Empty;

    [ProtoMember(3)]
    public PoseDto Pose { get; set; } = null;

    [ProtoMember(4)]
    public IEnumerable<ServiceType> Services { get; set; }
}