using System;
using System.Collections.Generic;

namespace GAAPICommon.Architecture
{
    public static class ExtensionMethods
    {
        public static bool IsSuccessful(this IServiceCallResult result)
        {
            if (result == null) throw new ArgumentNullException("result");

            return result.ServiceCode == (int)ServiceCode.NoError;
        }

        public static bool IsFault(this ExtendedDataFaultStatus exFaultStatus) => ExDataFaultStates.Contains(exFaultStatus);

        public static bool IsFault(this PositionControlStatus positionControlStatus) => PCSFaultStates.Contains(positionControlStatus);

        public static bool IsFault(this NavigationStatus navigationStatus) => NavigationFaultStates.Contains(navigationStatus);

        public static bool IsFault(this DynamicLimiterStatus dynamicLimiterStatus) => DynamicFaultStates.Contains(dynamicLimiterStatus);

        public static HashSet<NavigationStatus> NavigationFaultStates { get; } = new HashSet<NavigationStatus>()
        {
            NavigationStatus.AssociationFailure,
            NavigationStatus.HighUncertainty,
            NavigationStatus.Lost
        };

        public static HashSet<PositionControlStatus> PCSFaultStates { get; } = new HashSet<PositionControlStatus>()
        {
            PositionControlStatus.OutOfPosition,
            PositionControlStatus.WaypointDiscontinuity
        };

        public static HashSet<ExtendedDataFaultStatus> ExDataFaultStates { get; } = new HashSet<ExtendedDataFaultStatus>()
        {
            ExtendedDataFaultStatus.Fault
        };

        public static HashSet<DynamicLimiterStatus> DynamicFaultStates { get; } = new HashSet<DynamicLimiterStatus>()
        {
            DynamicLimiterStatus.MotorFault
        };
    }
}