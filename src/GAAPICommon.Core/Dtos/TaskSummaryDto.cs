using GAAPICommon.Architecture;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GAAPICommon.Core.Dtos
{
    [DataContract]
    public class TaskSummaryDto
    {
        [DataMember]
        public RoadmapItemTaskSummaryDto RoadmapItemTaskSummary { get; set; }

        [DataMember]
        public int? ParentTaskId { get; set; }

        [DataMember]
        public int TaskId { get; set; }

        [DataMember]
        public TaskStatus TaskStatus { get; set; }

        [DataMember]
        public TaskType TaskType { get; set; }
    }
}
