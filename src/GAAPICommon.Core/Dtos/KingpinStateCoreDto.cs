using GAAPICommon.Architecture;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos
{
    /// <summary>
    /// Serializable structure to represent the 'player' data of a kingpin.
    /// </summary>
    [DataContract]
    public class KingpinStateCoreDto : IKingpinStateCore
    {
        [DataMember]
        public string Alias { get; set; } = string.Empty;

        [DataMember]
        public bool IsVirtual { get; set; } = false;

        [DataMember]
        public MovementType CurrentMovementType { get; set; } = MovementType.Stationary;

        [DataMember]
        public byte Tick { get; set; } = 0;

        [DataMember]
        public AgvMode AgvMode { get; set; } = AgvMode.Manual;

        [DataMember]
        public double BatteryChargePercentage { get; set; } = 0.5f;

        [DataMember]
        public PositionControlStatus PositionControlStatus { get; set; } = PositionControlStatus.OK;

        [DataMember]
        public NavigationStatus NavigationStatus { get; set; } = NavigationStatus.OK;

        [DataMember]
        public DynamicLimiterStatus DynamicLimiterStatus { get; set; } = DynamicLimiterStatus.OK;

        [DataMember]
        public ExtendedDataFaultStatus ExtendedDataFaultStatus { get; set; } = ExtendedDataFaultStatus.OK;

        [DataMember]
        public FrozenState FrozenState { get; set; }

        [DataMember]
        public float Heading { get; set; } = float.NaN;

        [DataMember]
        public long IPAddress { get; set; }

        [DataMember]
        public bool IsCharging { get; set; } = false;

        [DataMember]
        public int LastCompletedInstructionId { get; set; } = int.MinValue;

        [DataMember]
        public double Speed { get; set; } = double.NaN;

        [DataMember]
        public byte[] StateCastExtendedData { get; set; } = new byte[0];

        [DataMember]
        public byte[] CurrentWaypointExtendedData { get; set; } = new byte[0];

        [DataMember]
        public TimeSpan Stationary { get; set; } = TimeSpan.MinValue;

        [DataMember]
        public int WaypointLastId { get; set; } = int.MinValue;

        [DataMember]
        public int WaypointNextId { get; set; } = int.MinValue;

        [DataMember]
        public float X { get; set; } = float.NaN;

        [DataMember]
        public float Y { get; set; } = float.NaN;
    }
}