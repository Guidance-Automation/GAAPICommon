using GAAPICommon.Architecture;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos
{
    [DataContract]
    public class SpotManagerStateDto : ISpotManagerState
    {
        [DataMember]
        public IEnumerable<ChargingSpotStateDto> ChargingSpotStateDtos { get; set; } = Enumerable.Empty<ChargingSpotStateDto>();

        [DataMember]
        public IEnumerable<ParkingSpotStateDto> ParkingSpotStateDtos { get; set; } = Enumerable.Empty<ParkingSpotStateDto>();

        public IEnumerable<IChargingSpotState> ChargingSpotStates => ChargingSpotStateDtos;

        public IEnumerable<IParkingSpotState> ParkingSpotStates => ParkingSpotStateDtos;

        [DataMember]
        public bool IsChanged { get; set; } = false;

        [DataMember]
        public byte Tick { get; set; }

        public override string ToString() => this.ToSummaryString();
    }
}
