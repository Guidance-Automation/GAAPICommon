using GAAPICommon.Architecture;
using ProtoBuf;

namespace GAAPICommon.Core.Dtos;

[ProtoContract]
public class ParkBookingStateDto : AbstractBookingStateDto, IParkBookingState
{
    public override string ToString() => this.ToBookingStateString();
}
