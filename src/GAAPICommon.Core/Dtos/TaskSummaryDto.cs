using GAAPICommon.Architecture;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos;

[DataContract]
public class TaskSummaryDto
{
    [DataMember]
    public RoadmapItemTaskSummaryDto? RoadmapItemTaskSummary { get; set; }

    [DataMember]
    public int? ParentTaskId { get; set; }

    [DataMember]
    public int TaskId { get; set; }

    [DataMember]
    public Architecture.TaskStatus TaskStatus { get; set; }

    [DataMember]
    public TaskType TaskType { get; set; }
}