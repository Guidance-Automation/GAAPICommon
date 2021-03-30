using System;
using System.Collections.Generic;
using System.Text;

namespace GAAPICommon.Architecture
{
    public interface IKingpinStateCore : IKingpinStatusReporter
    {
        string Alias { get; }

        bool IsVirtual { get; }

        byte Tick { get; }

        float X { get; }

        float Y { get; }

        float Heading { get; }

        MovementType CurrentMovementType { get; }

        long IPAddress { get; }

        byte[] StateCastExtendedData { get; }

        double Speed { get; }

        int WaypointLastId { get; }

        int WaypointNextId { get; }

        AgvMode AgvMode { get; }

        double BatteryChargePercentage { get; }

        ExtendedDataFaultStatus ExtendedDataFaultStatus { get; }

        FrozenState FrozenState { get; }

        bool IsCharging { get; }

        int LastCompletedInstructionId { get; }

        TimeSpan Stationary { get; }

        byte[] CurrentWaypointExtendedData { get; }
    }
}
