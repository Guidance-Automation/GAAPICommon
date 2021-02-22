using GAAPICommon.Architecture;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos
{
    [DataContract]
    public abstract class AbstractBookingStateDto
    {
        [DataMember]
        public int AgentId { get; set; }

        [DataMember]
        public BookingState BookingState { get; set; }

        [DataMember]
        public int JobId { get; set; }

        [DataMember]
        public int TaskId { get; set; }

        public string ToBookingSummary()
        {
            return $"AgentId:{AgentId} BookingState:{BookingState} JobId:{JobId} TaskId:{TaskId}";
        }

        public override string ToString() => ToBookingSummary();
    }
}
