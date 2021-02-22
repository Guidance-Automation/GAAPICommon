using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos
{
    [DataContract]
    public class ParkBookingDto : AbstractBookingStateDto
    {
        public override string ToString() => ToBookingSummary();
    }
}
