using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos;

public class PoseDto
{
    [DataMember]
    public double X { get; set; }

    [DataMember]
    public double Y { get; set; }

    [DataMember]
    public double Heading { get; set; }
}