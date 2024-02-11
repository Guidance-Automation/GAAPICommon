using ProtoBuf;

namespace GAAPICommon.Core.Dtos;

[ProtoContract]
public class PoseDto
{
    [ProtoMember(1)]
    public double X { get; set; }

    [ProtoMember(2)]
    public double Y { get; set; }

    [ProtoMember(3)]
    public double Heading { get; set; }
}