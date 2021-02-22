using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos
{
    [DataContract]
    public abstract class AbstractSpotStateDto
    {
        [DataMember]
        public int NodeId { get; set; }

        [DataMember]
        public bool IsBooked { get; set; }


        public string ToSummaryString()
        {
            return $"NodeId:{NodeId} IsBooked:{IsBooked}";
        }

        public override string ToString() => ToSummaryString();
    }
}
