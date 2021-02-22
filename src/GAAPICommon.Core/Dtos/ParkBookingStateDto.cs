using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos
{
    [DataContract]
    public class ParkBookingStateDto : AbstractBookingStateDto
    {
        public override string ToString() => ToBookingSummary();
    }
}
