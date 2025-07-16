using GAAPICommon.Enums;
using GAAPICommon.Interfaces;
using System.Net;
using System.Text.Json.Serialization;

namespace GAAPICommon.Constructors;

/// <summary>
/// Represents the state of a vehicle controller in the system, including details like movement,
/// mode, position, and network status. This class is used to serialize kingpin data for network communication.
/// </summary>
public class KingpinState : IKingpinState
{
    private bool _isInFault = false;

    /// <summary>
    /// Gets or sets the alias name for the kingpin.
    /// </summary>
    public string Alias { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the kingpin is virtual.
    /// </summary>
    public bool IsVirtual { get; set; } = false;

    /// <summary>
    /// Current type of movement the kingpin is performing.
    /// </summary>
    public MovementType CurrentMovementType { get; set; } = MovementType.Stationary;

    /// <summary>
    /// Tick value used for synchronization or versioning in control loops.
    /// </summary>
    public byte Tick { get; set; } = 0;

    /// <summary>
    /// Current operating mode of the AGV (Automated Guided Vehicle).
    /// </summary>
    public AgvMode AgvMode { get; set; } = AgvMode.Manual;

    /// <summary>
    /// Current battery charge level as a percentage.
    /// </summary>
    public double BatteryChargePercentage { get; set; } = 0.5f;

    /// <summary>
    /// Status of the kingpin's position control system.
    /// </summary>
    public PositionControlStatus PositionControlStatus { get; set; } = PositionControlStatus.Okposition;

    /// <summary>
    /// Navigation status of the kingpin.
    /// </summary>
    public NavigationStatus NavigationStatus { get; set; } = NavigationStatus.Oknavigation;

    /// <summary>
    /// Status from the dynamic limiter system, which could include various limits or errors.
    /// </summary>
    public DynamicLimiterStatus DynamicLimiterStatus { get; set; } = DynamicLimiterStatus.Ok;

    /// <summary>
    /// Status indicating any faults from extended data sources.
    /// </summary>
    public ExtendedDataFaultStatus ExtendedDataFaultStatus { get; set; } = ExtendedDataFaultStatus.NoFault;

    /// <summary>
    /// Current frozen state of the kingpin.
    /// </summary>
    public FrozenState FrozenState { get; set; }

    /// <summary>
    /// Current heading of the kingpin in degrees.
    /// </summary>
    public float Heading { get; set; } = float.NaN;

    /// <summary>
    /// IP address of the kingpin, serialized using a custom JSON converter.
    /// </summary>
    [JsonConverter(typeof(IPAddressJsonConverter))]
    public IPAddress? IPAddress { get; set; } = null;

    /// <summary>
    /// Indicates whether the kingpin is currently charging.
    /// </summary>
    public bool IsCharging { get; set; } = false;

    /// <summary>
    /// Identifier of the last completed instruction.
    /// </summary>
    public int LastCompletedInstructionId { get; set; } = int.MinValue;

    /// <summary>
    /// Current speed of the kingpin.
    /// </summary>
    public double Speed { get; set; } = double.NaN;

    /// <summary>
    /// Extended state data in a byte array, specific to custom implementations.
    /// </summary>
    public byte[] StateCastExtendedData { get; set; } = [];

    /// <summary>
    /// Extended data for the current waypoint in a byte array.
    /// </summary>
    public byte[] CurrentWaypointExtendedData { get; set; } = [];

    /// <summary>
    /// Duration for which the kingpin has been stationary.
    /// </summary>
    public TimeSpan Stationary { get; set; } = TimeSpan.MinValue;

    /// <summary>
    /// Identifier of the last waypoint.
    /// </summary>
    public int WaypointLastId { get; set; } = int.MinValue;

    /// <summary>
    /// Identifier for the next waypoint.
    /// </summary>
    public int WaypointNextId { get; set; } = int.MinValue;

    /// <summary>
    /// X-coordinate of the kingpin's current location.
    /// </summary>
    public float X { get; set; } = float.NaN;

    /// <summary>
    /// Y-coordinate of the kingpin's current location.
    /// </summary>
    public float Y { get; set; } = float.NaN;

    /// <summary>
    /// Binding check for if is in fault.
    /// </summary>
    public bool IsInFault
    {
        get
        {
            _isInFault = PositionControlStatus.IsFault()
                || NavigationStatus.IsFault()
                ||  DynamicLimiterStatus.IsFault()
                || ExtendedDataFaultStatus.IsFault();
            return _isInFault;
        }
        set
        {
            _isInFault = value;
        }
    }

    /// <summary>
    /// Current loaded state of the kingpin, indicating whether it is loaded, unloaded, or in transition.
    /// </summary>
    public LoadedState LoadedState { get; set; }

    /// <summary>
    /// Field used to propagate peripheral information, such as a barcode scan.
    /// </summary>
    public string PeripheralData { get; set; } = string.Empty;

    /// <summary>
    /// Used to track the number of payloads currently associated with the kingpin.
    /// </summary>
    public int PayloadCount { get; set; } = 0;
}