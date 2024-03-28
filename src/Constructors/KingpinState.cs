using GAAPICommon.Enums;
using GAAPICommon.Interfaces;
using System.Net;
using System.Text.Json.Serialization;

namespace GAAPICommon.Constructors;

/// <summary>
/// Serializable structure to represent the 'player' data of a kingpin.
/// </summary>
public class KingpinState : IKingpinState
{
    public string Alias { get; set; } = string.Empty;

    public bool IsVirtual { get; set; } = false;

    public MovementType CurrentMovementType { get; set; } = MovementType.Stationary;

    public byte Tick { get; set; } = 0;

    public AgvMode AgvMode { get; set; } = AgvMode.Manual;

    public double BatteryChargePercentage { get; set; } = 0.5f;

    public PositionControlStatus PositionControlStatus { get; set; } = PositionControlStatus.Okposition;

    public NavigationStatus NavigationStatus { get; set; } = NavigationStatus.Oknavigation;

    public DynamicLimiterStatus DynamicLimiterStatus { get; set; } = DynamicLimiterStatus.Ok;

    public ExtendedDataFaultStatus ExtendedDataFaultStatus { get; set; } = ExtendedDataFaultStatus.NoFault;

    public FrozenState FrozenState { get; set; }

    public float Heading { get; set; } = float.NaN;

    [JsonConverter(typeof(IPAddressJsonConverter))]
    public IPAddress? IPAddress { get; set; } = null;

    public bool IsCharging { get; set; } = false;

    public int LastCompletedInstructionId { get; set; } = int.MinValue;

    public double Speed { get; set; } = double.NaN;

    public byte[] StateCastExtendedData { get; set; } = [];

    public byte[] CurrentWaypointExtendedData { get; set; } = [];

    public TimeSpan Stationary { get; set; } = TimeSpan.MinValue;

    public int WaypointLastId { get; set; } = int.MinValue;

    public int WaypointNextId { get; set; } = int.MinValue;

    public float X { get; set; } = float.NaN;

    public float Y { get; set; } = float.NaN;
}