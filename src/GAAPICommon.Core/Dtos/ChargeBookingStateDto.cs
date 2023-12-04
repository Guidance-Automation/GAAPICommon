using GAAPICommon.Architecture;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos;

[DataContract]
public class ChargeBookingStateDto : AbstractBookingStateDto, IChargeBookingState
{
    [DataMember]
    public ChargeType ChargeType { get; set; }

    public override string ToString()
    {
        return this.ToChargeBookingStateString();
    }
}