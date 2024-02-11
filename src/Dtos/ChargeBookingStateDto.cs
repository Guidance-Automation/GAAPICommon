using GAAPICommon.Architecture;
using ProtoBuf;

namespace GAAPICommon.Core.Dtos;

[ProtoContract]
public class ChargeBookingStateDto : AbstractBookingStateDto, IChargeBookingState
{
    [ProtoMember(1)]
    public ChargeType ChargeType { get; set; }

    public override string ToString() => this.ToChargeBookingStateString();
}