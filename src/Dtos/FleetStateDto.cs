using GAAPICommon.Architecture;
using ProtoBuf;
using System.Linq;

namespace GAAPICommon.Core.Dtos;

[ProtoContract]
public class FleetStateDto
{
    public FleetStateDto()
    {
    }

    public FleetStateDto(byte tick, IKingpinState[] kingpinStates, FrozenState frozenState)
    {
        Tick = tick;
        KingpinStates = kingpinStates.Cast<KingpinStateDto>().ToArray();
        FrozenState = frozenState;
    }

    [ProtoMember(1)]
    public KingpinStateDto[] KingpinStates { get; set; } // Needs to be concrete for serialization

    [ProtoMember(2)]
    public byte Tick { get; set; }

    [ProtoMember(3)]
    public FrozenState FrozenState { get; set; }
}