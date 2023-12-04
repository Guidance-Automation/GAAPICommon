using GAAPICommon.Architecture;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos;

[DataContract]
public class ParkBookingStateDto : AbstractBookingStateDto, IParkBookingState
{
    public override string ToString()
    {
        return this.ToBookingStateString();
    }
}