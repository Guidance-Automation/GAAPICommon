using System.Collections.Generic;

namespace GAAPICommon.Architecture;

public interface ISpotManagerState
{
    public IEnumerable<IChargingSpotState> ChargingSpotStates { get; }

    public IEnumerable<IParkingSpotState> ParkingSpotStates { get; }

    public bool IsChanged { get; }

    public byte Tick { get; }
}