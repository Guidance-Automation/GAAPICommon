using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos
{
    [DataContract]
    public class RoadmapItemTaskSummaryDto
    {
        [DataMember]
        public int RoadmapItemId { get; set; }
    }
}
