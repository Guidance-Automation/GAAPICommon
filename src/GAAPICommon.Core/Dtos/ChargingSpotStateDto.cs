using GAAPICommon.Architecture;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos
{
    [DataContract]
    public class ChargingSpotStateDto : AbstractSpotStateDto, IChargingSpotState
    {
        [DataMember]
        public ChargeBookingStateDto ChargeBooking { get; set; } = null;


        public string ToChargingSpotStateString()
        {
            return ChargeBooking == null
                ? ToSummaryString()
                : $"{ToSummaryString()} {ChargeBooking.ToChargeBookingSummary()}";
        }

        public override string ToString() => ToChargingSpotStateString();
    }
}
