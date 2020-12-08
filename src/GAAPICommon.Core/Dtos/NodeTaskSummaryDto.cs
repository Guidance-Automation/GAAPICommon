using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos
{
    [DataContract]
    public class NodeTaskSummaryDto
    {
        [DataMember]
        public int NodeId { get; set; }
    }
}
