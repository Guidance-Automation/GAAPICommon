using GAAPICommon.Architecture;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos;

[DataContract]
public abstract class AbstractSpotStateDto : ISpotState
{
    [DataMember]
    public int NodeId { get; set; }

    [DataMember]
    public bool IsBooked { get; set; }

    public override string ToString()
    {
        return this.ToSpotStateString();
    }
}