using ProtoBuf;

namespace GAAPICommon.Core.Dtos;

public class MoveDto
{
    [ProtoMember(1)]
    public string Alias { get; set; }

    [ProtoMember(2)]
    public int DestinationId { get; set; }

    [ProtoMember(3)]
    public int Id { get; set; }

    [ProtoMember(4)]
    public int SourceId { get; set; }
}