using GAAPICommon.Architecture;
using System.Linq;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos
{
    [DataContract]
    public class FleetStateDto
    {
        public FleetStateDto(byte tick, IKingpinState[] kingpinStates)
        {
            Tick = tick;
            KingpinStates = kingpinStates.Cast<KingpinStateDto>().ToArray();
        }

        [DataMember]
        public KingpinStateDto[] KingpinStates { get; set; } // Needs to be concrete for serialization

        [DataMember]
        public byte Tick { get; set; }
    }
}