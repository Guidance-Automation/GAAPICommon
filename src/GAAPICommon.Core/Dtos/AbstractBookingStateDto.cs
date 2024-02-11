using GAAPICommon.Architecture;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos
{
    [DataContract]
    public abstract class AbstractBookingStateDto : IBookingState
    {
        [DataMember]
        public int AgentId { get; set; }

        [DataMember]
        public BookingState BookingState { get; set; }

        [DataMember]
        public int JobId { get; set; }

        [DataMember]
        public int TaskId { get; set; }

        public override string ToString() => this.ToBookingStateString();
    }
}
