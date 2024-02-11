using GAAPICommon.Architecture;
using ProtoBuf;
using System;
using System.Net;
using System.Text.Json.Serialization;

namespace GAAPICommon.Core.Dtos;

/// <summary>
/// Serializable structure to represent the 'player' data of a kingpin.
/// </summary>
[ProtoContract]
public class KingpinStateDto : IKingpinState
{
    [ProtoMember(1)]
    public string Alias { get; set; } = string.Empty;

    [ProtoMember(2)]
    public bool IsVirtual { get; set; } = false;

    [ProtoMember(3)]
    public MovementType CurrentMovementType { get; set; } = MovementType.Stationary;

    [ProtoMember(4)]
    public byte Tick { get; set; } = 0;

    [ProtoMember(5)]
    public AgvMode AgvMode { get; set; } = AgvMode.Manual;

    [ProtoMember(6)]
    public double BatteryChargePercentage { get; set; } = 0.5f;

    [ProtoMember(7)]
    public PositionControlStatus PositionControlStatus { get; set; } = PositionControlStatus.OK;

    [ProtoMember(8)]
    public NavigationStatus NavigationStatus { get; set; } = NavigationStatus.OK;

    [ProtoMember(9)]
    public DynamicLimiterStatus DynamicLimiterStatus { get; set; } = DynamicLimiterStatus.OK;

    [ProtoMember(10)]
    public ExtendedDataFaultStatus ExtendedDataFaultStatus { get; set; } = ExtendedDataFaultStatus.OK;

    [ProtoMember(11)]
    public FrozenState FrozenState { get; set; }

    [ProtoMember(12)]
    public float Heading { get; set; } = float.NaN;

    [ProtoMember(13)]
    [JsonConverter(typeof(IPAddressJsonConverter))]
    public IPAddress IPAddress { get; set; } = null;

    [ProtoMember(14)]
    public bool IsCharging { get; set; } = false;

    [ProtoMember(15)]
    public int LastCompletedInstructionId { get; set; } = int.MinValue;

    [ProtoMember(16)]
    public double Speed { get; set; } = double.NaN;

    [ProtoMember(17)]
    public byte[] StateCastExtendedData { get; set; } = [];

    [ProtoMember(18)]
    public byte[] CurrentWaypointExtendedData { get; set; } = [];

    [ProtoMember(19)]
    public TimeSpan Stationary { get; set; } = TimeSpan.MinValue;

    [ProtoMember(20)]
    public int WaypointLastId { get; set; } = int.MinValue;

    [ProtoMember(21)]
    public int WaypointNextId { get; set; } = int.MinValue;

    [ProtoMember(22)]
    public float X { get; set; } = float.NaN;

    [ProtoMember(23)]
    public float Y { get; set; } = float.NaN;
}