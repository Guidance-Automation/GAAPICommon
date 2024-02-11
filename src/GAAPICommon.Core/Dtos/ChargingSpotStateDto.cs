using GAAPICommon.Architecture;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos
{
    [DataContract]
    public class ChargingSpotStateDto : AbstractSpotStateDto, IChargingSpotState
    {
        [DataMember]
        public ChargeBookingStateDto ChargeBookingDto { get; set; } = null;

        public IChargeBookingState ChargeBooking => ChargeBookingDto;

        public override string ToString() => this.ToChargingSpotStateString();
    }
}
