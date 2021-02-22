using System.Collections.Generic;

namespace GAAPICommon.Architecture
{
    public interface ISpotManagerState
    {
        IEnumerable<IChargingSpotState> ChargingSpotStates { get; }

        IEnumerable<IParkingSpotState> ParkingSpotStates { get; }

        byte Tick { get; }
    }
}
