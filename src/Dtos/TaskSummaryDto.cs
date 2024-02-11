using GAAPICommon.Architecture;
using ProtoBuf;

namespace GAAPICommon.Core.Dtos;

[ProtoContract]
public class TaskSummaryDto
{
    [ProtoMember(1)]
    public RoadmapItemTaskSummaryDto RoadmapItemTaskSummary { get; set; }

    [ProtoMember(2)]
    public int? ParentTaskId { get; set; }

    [ProtoMember(3)]
    public int TaskId { get; set; }

    [ProtoMember(4)]
    public TaskStatus TaskStatus { get; set; }

    [ProtoMember(5)]
    public TaskType TaskType { get; set; }
}
