using GAAPICommon.Architecture;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace GAAPICommon.Core.Dtos
{
    [DataContract]
    public class SpotManagerStateDto : ISpotManagerState
    {
        [DataMember]
        public IEnumerable<IChargingSpotState> ChargingSpotStates { get; set; } = Enumerable.Empty<IChargingSpotState>();
        [DataMember]
        public IEnumerable<IParkingSpotState> ParkingSpotStates { get; set;  } = Enumerable.Empty<IParkingSpotState>();

        [DataMember]
        public byte Tick { get; set; }

        public string ToSummaryString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine($"Tick:{Tick}");

            if (ChargingSpotStates != null && ChargingSpotStates.Any())
            {
                builder.AppendLine($"\tCharging Spots");
                foreach (ChargingSpotStateDto chargerState in ChargingSpotStates)
                {
                    builder.AppendLine($"\t\t{chargerState.ToChargingSpotStateString()}");
                }
            }

            if (ParkingSpotStates != null && ParkingSpotStates.Any())
            {
                builder.AppendLine($"\tParking Spots");
                foreach (ParkingSpotStateDto parkingState in ParkingSpotStates)
                {
                    builder.AppendLine($"\t\t{parkingState.ToParkingSpotStateString()}");
                }
            }

            return builder.ToString();
        }

        public override string ToString() => ToSummaryString();
    }
}
