using GAAPICommon.Architecture;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos
{
    [DataContract]
    public class ChargeBookingStateDto : AbstractBookingStateDto
    {
        [DataMember]
        public ChargeType ChargeType { get; set; }

        public string ToChargeBookingSummary()
        {
            return $"{ToBookingSummary()} ChargeType:{ChargeType}";
        }

        public override string ToString() => ToChargeBookingSummary();
    }
}
