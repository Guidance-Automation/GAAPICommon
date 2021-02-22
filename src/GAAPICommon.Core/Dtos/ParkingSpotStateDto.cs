using GAAPICommon.Architecture;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos
{
    [DataContract]
    public class ParkingSpotStateDto : AbstractSpotStateDto, IParkingSpotState
    {
        [DataMember]
        public ParkBookingStateDto ParkBookingDto { get; set; } = null;

        public IParkBookingState ParkBooking => ParkBookingDto;

        public override string ToString() => this.ToParkingSpotStateString();
    }
}
