using GAAPICommon.Architecture;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos
{
    [DataContract]
    public class ParkingSpotStateDto : AbstractSpotStateDto, IParkingSpotState
    {
        [DataMember]
        public ParkBookingStateDto ParkBooking { get; set; } = null;

        public string ToParkingSpotStateString()
        {
            return ParkBooking == null
                ? ToSummaryString()
                : $"{ToSummaryString()} {ParkBooking.ToBookingSummary()}";
        }

        public override string ToString() => ToParkingSpotStateString();
    }
}
