using GAAPICommon.Enums;
using GAAPICommon.Messages;
using System.Net;
using System.Text;

namespace GAAPICommon;

public static class ExtensionMethods
{
    public static bool IsInFault(this KingpinStateDto kingpinState)
    {
        if (kingpinState == null)
            return false;

        return kingpinState.PositionControlStatus.IsFault()
            || kingpinState.NavigationStatus.IsFault()
            || kingpinState.DynamicLimiterStatus.IsFault()
            || kingpinState.ExtendedDataFaultStatus.IsFault();
    }

    public static string ToChargeBookingStateString(this ChargeBookingStateDto chargeBookingState)
    {
        if (chargeBookingState == null)
            return string.Empty;

        return $"{chargeBookingState.BookingState} {chargeBookingState.ChargeType} {chargeBookingState.AgentId} ChargeType:{chargeBookingState.ChargeType}";
    }

    public static bool IsFault(this ExtendedDataFaultStatus exFaultStatus) => ExDataFaultStates.Contains(exFaultStatus);

    public static bool IsFault(this PositionControlStatus positionControlStatus) => PCSFaultStates.Contains(positionControlStatus);

    public static bool IsFault(this NavigationStatus navigationStatus) => NavigationFaultStates.Contains(navigationStatus);

    public static bool IsFault(this DynamicLimiterStatus dynamicLimiterStatus) => DynamicFaultStates.Contains(dynamicLimiterStatus);

    public static HashSet<NavigationStatus> NavigationFaultStates { get; } =
    [
        NavigationStatus.AssociationFailure,
        NavigationStatus.HighUncertainty,
        NavigationStatus.Lost,
        NavigationStatus.NoScannerData
    ];

    public static HashSet<PositionControlStatus> PCSFaultStates { get; } =
    [
        PositionControlStatus.OutOfPosition,
        PositionControlStatus.WaypointDiscontinuity
    ];

    public static HashSet<ExtendedDataFaultStatus> ExDataFaultStates { get; } =
    [
        ExtendedDataFaultStatus.Fault
    ];

    public static HashSet<DynamicLimiterStatus> DynamicFaultStates { get; } =
    [
        DynamicLimiterStatus.MotorFault
    ];

    public static byte[] ToBytes(this KeyedSpeedDemandDto keyedSpeedDemand)
    {
        ArgumentNullException.ThrowIfNull(keyedSpeedDemand);

        byte[] bytes = new byte[27];

        bytes[0] = (byte)keyedSpeedDemand.Tick;
        Guid guid = Guid.Parse(keyedSpeedDemand.Guid);
        guid.ToByteArray().CopyTo(bytes, 1);
        keyedSpeedDemand.SpeedDemand.ToBytes().CopyTo(bytes, 17);

        return bytes;
    }

    public static byte[] ToBytes(this SpeedDemandDto speedDemand)
    {
        ArgumentNullException.ThrowIfNull(speedDemand);

        byte[] bytes = new byte[10];

        IPAddress iPAddress = IPAddress.Parse(speedDemand.IPAddress);
        iPAddress.GetAddressBytes().CopyTo(bytes, 0);

        BitConverter.GetBytes(speedDemand.Forward).CopyTo(bytes, 4);
        BitConverter.GetBytes(speedDemand.Angular).CopyTo(bytes, 6);
        BitConverter.GetBytes(speedDemand.Lateral).CopyTo(bytes, 8);

        return bytes;
    }

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
}