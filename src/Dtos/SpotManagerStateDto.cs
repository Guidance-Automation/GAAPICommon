using GAAPICommon.Architecture;
using ProtoBuf;
using System.Collections.Generic;
using System.Linq;

namespace GAAPICommon.Core.Dtos;

[ProtoContract]
public class SpotManagerStateDto : ISpotManagerState
{
    [ProtoMember(1)]
    public IEnumerable<ChargingSpotStateDto> ChargingSpotStateDtos { get; set; } = Enumerable.Empty<ChargingSpotStateDto>();

    [ProtoMember(2)]
    public IEnumerable<ParkingSpotStateDto> ParkingSpotStateDtos { get; set; } = Enumerable.Empty<ParkingSpotStateDto>();

    public IEnumerable<IChargingSpotState> ChargingSpotStates => ChargingSpotStateDtos;

    public IEnumerable<IParkingSpotState> ParkingSpotStates => ParkingSpotStateDtos;

    [ProtoMember(3)]
    public bool IsChanged { get; set; } = false;

    [ProtoMember(4)]
    public byte Tick { get; set; }

    public override string ToString() => this.ToSummaryString();
}
