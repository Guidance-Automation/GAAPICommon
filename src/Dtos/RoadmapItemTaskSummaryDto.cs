using ProtoBuf;

namespace GAAPICommon.Core.Dtos;

[ProtoContract]
public class RoadmapItemTaskSummaryDto
{
    [ProtoMember(1)]
    public int RoadmapItemId { get; set; }
}
