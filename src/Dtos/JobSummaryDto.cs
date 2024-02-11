using GAAPICommon.Architecture;
using ProtoBuf;
using System.Collections.Generic;

namespace GAAPICommon.Core.Dtos;

[ProtoContract]
public class JobSummaryDto
{
    public JobSummaryDto()
    {
    }

    [ProtoMember(1)]
    public int AssignedAgentId { get; set; }

    [ProtoMember(2)]
    public string Description { get; set; }

    [ProtoMember(3)]
    public int JobId { get; set; }

    [ProtoMember(4)]
    public JobPriority JobPriority { get; set; }

    [ProtoMember(5)]
    public JobStatus JobStatus { get; set; }

    [ProtoMember(6)]
    public IEnumerable<string> Notes { get; set; }

    [ProtoMember(7)]
    public int RootOrderedListTaskId { get; set; }

    [ProtoMember(8)]
    public IEnumerable<TaskSummaryDto> TaskSummaries { get; set; }
}