using GAAPICommon.Architecture;
using ProtoBuf;

namespace GAAPICommon.Core.Dtos;

[ProtoContract]
public class ChargingSpotStateDto : AbstractSpotStateDto, IChargingSpotState
{
    [ProtoMember(1)]
    public ChargeBookingStateDto ChargeBookingDto { get; set; } = null;

    public IChargeBookingState ChargeBooking => ChargeBookingDto;

    public override string ToString() => this.ToChargingSpotStateString();
}
