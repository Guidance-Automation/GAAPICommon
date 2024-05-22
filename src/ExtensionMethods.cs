using GAAPICommon.Constructors;
using GAAPICommon.Enums;
using GAAPICommon.Interfaces;
using GAAPICommon.Messages;
using System.Net;
using System.Text;

namespace GAAPICommon;

/// <summary>
/// Provides extension methods for various data types used in moNitrav.
/// </summary>
public static class ExtensionMethods
{
    /// <summary>
    /// Determines if any of the status properties in the kingpin state indicate a fault.
    /// </summary>
    /// <param name="kingpinState">The kingpin state to check for faults.</param>
    /// <returns>True if any status property indicates a fault; otherwise, false.</returns>
    public static bool IsInFault(this IKingpinState kingpinState)
    {
        if (kingpinState == null)
        {
            return false;
        }

        bool isInFault = kingpinState.PositionControlStatus.IsFault()
            || kingpinState.NavigationStatus.IsFault()
            || kingpinState.DynamicLimiterStatus.IsFault()
            || kingpinState.ExtendedDataFaultStatus.IsFault();

        kingpinState.IsInFault = isInFault;
        return isInFault;
    }

    /// <summary>
    /// Converts a <see cref="ChargeBookingStateDto"/> object to a string representation that includes
    /// the booking state, charge type, and agent ID.
    /// </summary>
    /// <param name="chargeBookingState">The charge booking state to convert to string.</param>
    /// <returns>A string representation of the charge booking state.</returns>
    public static string ToChargeBookingStateString(this ChargeBookingStateDto chargeBookingState)
    {
        if (chargeBookingState == null)
            return string.Empty;

        return $"{chargeBookingState.BookingState} {chargeBookingState.ChargeType} {chargeBookingState.AgentId} ChargeType:{chargeBookingState.ChargeType}";
    }

    /// <summary>
    /// Checks if the given <see cref="ExtendedDataFaultStatus"/> indicates a fault.
    /// </summary>
    public static bool IsFault(this ExtendedDataFaultStatus exFaultStatus) => ExDataFaultStates.Contains(exFaultStatus);

    /// <summary>
    /// Checks if the given <see cref="PositionControlStatus"/> indicates a fault.
    /// </summary>
    public static bool IsFault(this PositionControlStatus positionControlStatus) => PCSFaultStates.Contains(positionControlStatus);

    /// <summary>
    /// Checks if the given <see cref="NavigationStatus"/> indicates a fault.
    /// </summary>
    public static bool IsFault(this NavigationStatus navigationStatus) => NavigationFaultStates.Contains(navigationStatus);

    /// <summary>
    /// Checks if the given <see cref="DynamicLimiterStatus"/> indicates a fault.
    /// </summary>
    public static bool IsFault(this DynamicLimiterStatus dynamicLimiterStatus) => DynamicFaultStates.Contains(dynamicLimiterStatus);

    /// <summary>
    /// Static property defining which navigation statuses are considered faults.
    /// </summary>
    public static HashSet<NavigationStatus> NavigationFaultStates { get; } =
    [
        NavigationStatus.AssociationFailure,
        NavigationStatus.HighUncertainty,
        NavigationStatus.Lost,
        NavigationStatus.NoScannerData
    ];

    /// <summary>
    /// Static property defining which position control statuses are considered faults.
    /// </summary>
    public static HashSet<PositionControlStatus> PCSFaultStates { get; } =
    [
        PositionControlStatus.OutOfPosition,
        PositionControlStatus.WaypointDiscontinuity
    ];

    /// <summary>
    /// Static property defining which extended data fault statuses are considered actual faults.
    /// </summary>
    public static HashSet<ExtendedDataFaultStatus> ExDataFaultStates { get; } =
    [
        ExtendedDataFaultStatus.Fault
    ];

    /// <summary>
    /// Static property defining which dynamic limiter statuses are considered faults.
    /// </summary>
    public static HashSet<DynamicLimiterStatus> DynamicFaultStates { get; } =
    [
        DynamicLimiterStatus.MotorFault
    ];

    /// <summary>
    /// Converts a <see cref="KeyedSpeedDemand"/> object to a byte array suitable for serialization.
    /// </summary>
    public static byte[] ToBytes(this KeyedSpeedDemand keyedSpeedDemand)
    {
        ArgumentNullException.ThrowIfNull(keyedSpeedDemand);

        byte[] bytes = new byte[27];

        bytes[0] = keyedSpeedDemand.Tick;
        keyedSpeedDemand.Guid.ToByteArray().CopyTo(bytes, 1);
        keyedSpeedDemand.SpeedDemand?.ToBytes().CopyTo(bytes, 17);

        return bytes;
    }

    /// <summary>
    /// Converts a <see cref="SpeedDemand"/> object to a byte array suitable for serialization.
    /// </summary>
    public static byte[] ToBytes(this SpeedDemand speedDemand)
    {
        ArgumentNullException.ThrowIfNull(speedDemand);

        byte[] bytes = new byte[10];

        speedDemand.IPAddress?.GetAddressBytes().CopyTo(bytes, 0);
        BitConverter.GetBytes(speedDemand.Forward).CopyTo(bytes, 4);
        BitConverter.GetBytes(speedDemand.Angular).CopyTo(bytes, 6);
        BitConverter.GetBytes(speedDemand.Lateral).CopyTo(bytes, 8);

        return bytes;
    }

    /// <summary>
    /// Converts a <see cref="ServiceCodeDefinitionDto"/> to a summary string that details its contents.
    /// </summary>
    public static string ToSummary(this ServiceCodeDefinitionDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        StringBuilder builder = new();

        builder.AppendLine("Service Code Definition:");
        builder.AppendLine($"\tService code: {dto.ServiceCode}");
        builder.AppendLine($"\tMessage: {dto.Message}");
        builder.AppendLine($"\tDescription: {dto.Description}");
        builder.Append($"\tSolution: {dto.Solution}");

        return builder.ToString();
    }

    /// <summary>
    /// Converts a <see cref="KingpinStateDto"/> to an <see cref="IKingpinState"/> instance.
    /// </summary>
    public static IKingpinState ToKingpinState(this KingpinStateDto dto)
    {
        return new KingpinState
        {
            Alias = dto.Alias,
            IsVirtual = dto.IsVirtual,
            CurrentMovementType = dto.CurrentMovementType,
            Tick = Convert.ToByte(dto.Tick),
            AgvMode = dto.AgvMode,
            BatteryChargePercentage = dto.BatteryChargePercentage,
            PositionControlStatus = dto.PositionControlStatus,
            NavigationStatus = dto.NavigationStatus,
            DynamicLimiterStatus = dto.DynamicLimiterStatus,
            ExtendedDataFaultStatus = dto.ExtendedDataFaultStatus,
            FrozenState = dto.FrozenState,
            Heading = dto.Heading,
            IPAddress = IPAddress.Parse(dto.IPAddress),
            IsCharging = dto.IsCharging,
            LastCompletedInstructionId = dto.LastCompletedInstructionId,
            Speed = dto.Speed,
            StateCastExtendedData = dto.StateCastExtendedData.ToByteArray(),
            CurrentWaypointExtendedData = dto.CurrentWaypointExtendedData.ToByteArray(),
            Stationary = dto.Stationary.ToTimeSpan(),
            WaypointLastId = dto.WaypointLastId,
            WaypointNextId = dto.WaypointNextId,
            X = dto.X,
            Y = dto.Y
        };
    }

    /// <summary>
    /// Converts an <see cref="IKingpinState"/> to a <see cref="KingpinStateDto"/>.
    /// </summary>
    public static KingpinStateDto ToKingpinStateDto(this IKingpinState state)
    {
        return new KingpinStateDto
        {
            Alias = state.Alias,
            IsVirtual = state.IsVirtual,
            CurrentMovementType = state.CurrentMovementType,
            Tick = Convert.ToByte(state.Tick),
            AgvMode = state.AgvMode,
            BatteryChargePercentage = state.BatteryChargePercentage,
            PositionControlStatus = state.PositionControlStatus,
            NavigationStatus = state.NavigationStatus,
            DynamicLimiterStatus = state.DynamicLimiterStatus,
            ExtendedDataFaultStatus = state.ExtendedDataFaultStatus,
            FrozenState = state.FrozenState,
            Heading = state.Heading,
            IPAddress = state.IPAddress?.ToString(),
            IsCharging = state.IsCharging,
            LastCompletedInstructionId = state.LastCompletedInstructionId,
            Speed = state.Speed,
            StateCastExtendedData = Google.Protobuf.ByteString.CopyFrom(state.StateCastExtendedData),
            CurrentWaypointExtendedData = Google.Protobuf.ByteString.CopyFrom(state.CurrentWaypointExtendedData),
            Stationary = Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(state.Stationary),
            WaypointLastId = state.WaypointLastId,
            WaypointNextId = state.WaypointNextId,
            X = state.X,
            Y = state.Y
        };
    }

    /// <summary>
    /// Converts a <see cref="FleetStateDto"/> to a <see cref="FleetState"/>.
    /// </summary>
    public static FleetState ToFleetState(this FleetStateDto dto)
    {
        return new FleetState
        {
            KingpinStates = Array.ConvertAll(dto.KingpinStates.ToArray(), e => (KingpinState)e.ToKingpinState()),
            Tick = Convert.ToByte(dto.Tick),
            FrozenState = dto.FrozenState
        };
    }

    /// <summary>
    /// Converts a <see cref="FleetState"/> to a <see cref="FleetStateDto"/>.
    /// </summary>
    public static FleetStateDto ToFleetStateDto(this FleetState dto)
    {
        FleetStateDto newDto = new()
        {
            Tick = dto.Tick,
            FrozenState = dto.FrozenState
        };
        KingpinStateDto[] states = Array.ConvertAll(dto.KingpinStates ?? [], e => e.ToKingpinStateDto());
        newDto.KingpinStates.AddRange(states);
        return newDto;
    }
}