using GAAPICommon.Enums;

namespace GAAPICommon.Constructors;

/// <summary>
/// Represents the collective state of a fleet, encapsulating the states of individual kingpins within the fleet,
/// a synchronization tick, and the overall frozen state of the fleet.
/// </summary>
public class FleetState
{
    /// <summary>
    /// Gets or sets an array of <see cref="KingpinState"/> objects, each representing the state
    /// of an individual kingpin within the fleet. This array can be null if no kingpin states are available.
    /// </summary>
    public KingpinState[]? KingpinStates { get; set; }

    /// <summary>
    /// Gets or sets the tick value used for synchronization or versioning across the fleet state updates.
    /// This value is used to ensure consistency and ordering of state updates.
    /// </summary>
    public byte Tick { get; set; }

    /// <summary>
    /// Gets or sets the frozen state of the fleet. The <see cref="Enums.FrozenState"/> indicates
    /// whether the fleet is currently halted (frozen) or active.
    /// </summary>
    public FrozenState FrozenState { get; set; }
}