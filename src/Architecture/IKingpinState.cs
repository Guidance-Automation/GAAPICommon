using System;
using System.Net;

namespace GAAPICommon.Architecture;

public interface IKingpinState : IKingpinStatusReporter
{
    public string Alias { get; }

    public bool IsVirtual { get; }

    public byte Tick { get; }

    public float X { get; }

    public float Y { get; }

    public float Heading { get; }

    public MovementType CurrentMovementType { get; }

    public IPAddress IPAddress { get; }

    public byte[] StateCastExtendedData { get; }

    public double Speed { get; }

    public int WaypointLastId { get; }

    public int WaypointNextId { get; }

    public AgvMode AgvMode { get; }

    public double BatteryChargePercentage { get; }

    public ExtendedDataFaultStatus ExtendedDataFaultStatus { get; }

    public FrozenState FrozenState { get; }

    public bool IsCharging { get; }

    public int LastCompletedInstructionId { get; }

    public TimeSpan Stationary { get; }

    public byte[] CurrentWaypointExtendedData { get; }
}