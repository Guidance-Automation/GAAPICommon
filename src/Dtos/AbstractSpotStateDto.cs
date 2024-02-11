using GAAPICommon.Architecture;
using ProtoBuf;

namespace GAAPICommon.Core.Dtos;

[ProtoContract]
public abstract class AbstractSpotStateDto : ISpotState
{
    [ProtoMember(1)]
    public int NodeId { get; set; }

    [ProtoMember(2)]
    public bool IsBooked { get; set; }

    public override string ToString() => this.ToSpotStateString();
}