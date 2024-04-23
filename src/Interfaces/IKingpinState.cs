using GAAPICommon.Enums;
using System.Net;

namespace GAAPICommon.Interfaces;

/// <summary>
/// Defines the necessary state and operational status properties for a kingpin within the fleet.
/// </summary>
public interface IKingpinState : IKingpinStatusReporter
{
    /// <summary>
    /// The alias or name assigned to the kingpin.
    /// </summary>
    public string Alias { get; }

    /// <summary>
    /// Indicates whether the kingpin is a virtual or physical entity.
    /// </summary>
    public bool IsVirtual { get; }

    /// <summary>
    /// A tick value for synchronization or timestamping in updates.
    /// </summary>
    public byte Tick { get; }

    /// <summary>
    /// The X-coordinate of the kingpin's current position.
    /// </summary>
    public float X { get; }

    /// <summary>
    /// The Y-coordinate of the kingpin's current position.
    /// </summary>
    public float Y { get; }

    /// <summary>
    /// The current heading of the kingpin in degrees.
    /// </summary>
    public float Heading { get; }

    /// <summary>
    /// The current type of movement the kingpin is performing.
    /// </summary>
    public MovementType CurrentMovementType { get; }

    /// <summary>
    /// The IP address of the kingpin, if applicable.
    /// </summary>
    public IPAddress? IPAddress { get; }

    /// <summary>
    /// A byte array containing extended state data specific to the kingpin's current state.
    /// </summary>
    public byte[] StateCastExtendedData { get; }

    /// <summary>
    /// The current speed of the kingpin.
    /// </summary>
    public double Speed { get; }

    /// <summary>
    /// The identifier of the last waypoint that was successfully reached by the kingpin.
    /// </summary>
    public int WaypointLastId { get; }

    /// <summary>
    /// The identifier of the next target waypoint for the kingpin.
    /// </summary>
    public int WaypointNextId { get; }

    /// <summary>
    /// The current operating mode of the kingpin's automated guided vehicle (AGV) system.
    /// </summary>
    public AgvMode AgvMode { get; }

    /// <summary>
    /// The current battery charge level as a percentage.
    /// </summary>
    public double BatteryChargePercentage { get; }

    /// <summary>
    /// The status indicating any faults from extended data sources.
    /// </summary>
    public ExtendedDataFaultStatus ExtendedDataFaultStatus { get; }

    /// <summary>
    /// The operational status indicating whether the kingpin is currently halted (frozen) or active.
    /// </summary>
    public FrozenState FrozenState { get; }

    /// <summary>
    /// Indicates whether the kingpin is currently charging.
    /// </summary>
    public bool IsCharging { get; }

    /// <summary>
    /// The identifier of the last instruction that was completed by the kingpin.
    /// </summary>
    public int LastCompletedInstructionId { get; }

    /// <summary>
    /// The duration for which the kingpin has remained stationary.
    /// </summary>
    public TimeSpan Stationary { get; }

    /// <summary>
    /// A byte array containing extended data specific to the current waypoint.
    /// </summary>
    public byte[] CurrentWaypointExtendedData { get; }
}