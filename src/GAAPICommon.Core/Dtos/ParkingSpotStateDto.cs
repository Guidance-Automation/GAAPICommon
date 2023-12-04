using GAAPICommon.Architecture;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos;

[DataContract]
public class ParkingSpotStateDto : AbstractSpotStateDto, IParkingSpotState
{
    [DataMember]
    public ParkBookingStateDto? ParkBookingDto { get; set; }

    public IParkBookingState? ParkBooking
    {
        get
        {
            return ParkBookingDto;
        }
    }

    public override string ToString()
    {
        return this.ToParkingSpotStateString();
    }
}
