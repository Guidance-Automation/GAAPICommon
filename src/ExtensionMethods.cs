using System.Text;

namespace GAAPICommon;

public static class ExtensionMethods
{
    public static bool IsInFault(this IKingpinState kingpinState)
    {
        if (kingpinState == null)
            return false;

        return kingpinState.PositionControlStatus.IsFault()
            || kingpinState.NavigationStatus.IsFault()
            || kingpinState.DynamicLimiterStatus.IsFault()
            || kingpinState.ExtendedDataFaultStatus.IsFault();
    }

    public static string ToBookingStateString(this IBookingState bookingState)
    {
        if (bookingState == null)
            return string.Empty;

        return $"AgentId:{bookingState.AgentId} BookingState:{bookingState.BookingState} JobId:{bookingState.JobId} TaskId:{bookingState.TaskId}";
    }

    public static string ToChargeBookingStateString(this IChargeBookingState chargeBookingState)
    {
        if (chargeBookingState == null)
            return string.Empty;

        return $"{chargeBookingState.ToBookingStateString()} ChargeType:{chargeBookingState.ChargeType}";
    }

    public static string ToSpotStateString(this ISpotState spotState)
    {
        if (spotState == null)
            return string.Empty;

        return $"NodeId:{spotState.NodeId} IsBooked:{spotState.IsBooked}";
    }

    public static string ToChargingSpotStateString(this IChargingSpotState chargingSpotState)
    {
        if (chargingSpotState == null)
            return string.Empty;

        return chargingSpotState.ChargeBooking == null
            ? chargingSpotState.ToSpotStateString()
            : $"{chargingSpotState.ToSpotStateString()} {chargingSpotState.ChargeBooking.ToChargeBookingStateString()}";
    }

    public static string ToParkingSpotStateString(this IParkingSpotState parkingSpotState)
    {
        if (parkingSpotState == null)
            return string.Empty;

        return parkingSpotState.ParkBooking == null
            ? parkingSpotState.ToSpotStateString()
            : $"{parkingSpotState.ToSpotStateString()} {parkingSpotState.ParkBooking.ToBookingStateString()}";
    }


    public static string ToSummaryString(this ISpotManagerState spotManagerState)
    {
        ArgumentNullException.ThrowIfNull(spotManagerState);

        StringBuilder builder = new();

        builder.AppendLine($"Tick:{spotManagerState.Tick}");

        if (spotManagerState.ChargingSpotStates != null && spotManagerState.ChargingSpotStates.Any())
        {
            builder.AppendLine($"\tCharging Spots");
            foreach (IChargingSpotState chargerState in spotManagerState.ChargingSpotStates)
            {
                builder.AppendLine($"\t\t{chargerState.ToChargingSpotStateString()}");
            }
        }

        if (spotManagerState.ParkingSpotStates != null && spotManagerState.ParkingSpotStates.Any())
        {
            builder.AppendLine($"\tParking Spots");
            foreach (IParkingSpotState parkingState in spotManagerState.ParkingSpotStates)
            {
                builder.AppendLine($"\t\t{parkingState.ToParkingSpotStateString()}");
            }
        }

        return builder.ToString();
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
}