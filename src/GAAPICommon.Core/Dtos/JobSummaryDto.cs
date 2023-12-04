using GAAPICommon.Architecture;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos;

[DataContract]
public class JobSummaryDto
{
    [DataMember]
    public int AssignedAgentId { get; set; }

    [DataMember]
    public string? Description { get; set; }

    [DataMember]
    public int JobId { get; set; }

    [DataMember]
    public JobPriority JobPriority { get; set; }

    [DataMember]
    public JobStatus JobStatus { get; set; }

    [DataMember]
    public IEnumerable<string>? Notes { get; set; }

    [DataMember]
    public int RootOrderedListTaskId { get; set; }

    [DataMember]
    public IEnumerable<TaskSummaryDto>? TaskSummaries { get; set; }
}