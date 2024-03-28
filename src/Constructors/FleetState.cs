using GAAPICommon.Enums;

namespace GAAPICommon.Constructors;

public class FleetState
{
    public KingpinState[]? KingpinStates { get; set; } 

    public byte Tick { get; set; }

    public FrozenState FrozenState { get; set; }
}