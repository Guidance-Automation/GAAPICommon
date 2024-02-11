using GAAPICommon.Architecture;
using ProtoBuf;
using System.Linq;

namespace GAAPICommon.Core.Dtos;

[ProtoContract]
public class FleetStateCoreDto
{
    public FleetStateCoreDto()
    {
    }

    public FleetStateCoreDto(byte tick, KingpinStateCoreDto[] kingpinStatesCore, FrozenState frozenState)
    {
        Tick = tick;
        KingpinStates = kingpinStatesCore.Cast<KingpinStateCoreDto>().ToArray();
        FrozenState = frozenState;
    }

    [ProtoMember(1)]
    public KingpinStateCoreDto[] KingpinStates { get; set; } // Needs to be concrete for serialization

    [ProtoMember(2)]
    public byte Tick { get; set; }

    [ProtoMember(3)]
    public FrozenState FrozenState { get; set; }
}