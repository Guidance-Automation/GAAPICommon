using GAAPICommon.Architecture;
using ProtoBuf;

namespace GAAPICommon.Core.Dtos;

[ProtoContract]
public abstract class AbstractBookingStateDto : IBookingState
{
    [ProtoMember(1)]
    public int AgentId { get; set; }

    [ProtoMember(2)]
    public BookingState BookingState { get; set; }

    [ProtoMember(3)]
    public int JobId { get; set; }

    [ProtoMember(4)]
    public int TaskId { get; set; }

    public override string ToString() => this.ToBookingStateString();
}