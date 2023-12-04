using GAAPICommon.Architecture;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos;

public class NodeDto
{
    [DataMember]
    public int Id { get; set; } = -1;

    [DataMember]
    public string Alias { get; set; } = string.Empty;

    [DataMember]
    public PoseDto? Pose { get; set; }

    [DataMember]
    public IEnumerable<ServiceType>? Services { get; set; }
}