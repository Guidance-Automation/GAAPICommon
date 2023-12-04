using GAAPICommon.Architecture;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos;

[DataContract]
public class ChargingSpotStateDto : AbstractSpotStateDto, IChargingSpotState
{
    [DataMember]
    public ChargeBookingStateDto? ChargeBookingDto { get; set; }

    public IChargeBookingState? ChargeBooking
    {
        get
        {
            return ChargeBookingDto;
        }
    }

    public override string ToString()
    {
        return this.ToChargingSpotStateString();
    }
}