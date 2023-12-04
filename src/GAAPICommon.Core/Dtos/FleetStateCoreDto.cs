using GAAPICommon.Architecture;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos;

[DataContract]
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

    [DataMember]
    public KingpinStateCoreDto[]? KingpinStates { get; set; } // Needs to be concrete for serialization

    [DataMember]
    public byte Tick { get; set; }

    [DataMember]
    public FrozenState FrozenState { get; set; }
}