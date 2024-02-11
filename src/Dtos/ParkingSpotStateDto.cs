using GAAPICommon.Architecture;
using ProtoBuf;

namespace GAAPICommon.Core.Dtos;

[ProtoContract]
public class ParkingSpotStateDto : AbstractSpotStateDto, IParkingSpotState
{
    [ProtoMember(1)]
    public ParkBookingStateDto ParkBookingDto { get; set; } = null;

    public IParkBookingState ParkBooking => ParkBookingDto;

    public override string ToString() => this.ToParkingSpotStateString();
}