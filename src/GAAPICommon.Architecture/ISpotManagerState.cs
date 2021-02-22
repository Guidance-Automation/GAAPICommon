using System;
using System.Collections.Generic;
using System.Text;

namespace GAAPICommon.Architecture
{
    public interface ISpotManagerState
    {
        IEnumerable<IChargingSpotState> ChargingSpotStates { get; }

        IEnumerable<IParkingSpotState> ParkingSpotStates { get; }

        byte Tick { get; }
    }
}
