using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos;

public class MoveDto
{
    [DataMember]
    public string? Alias { get; set; }

    [DataMember]
    public int DestinationId { get; set; }

    [DataMember]
    public int Id { get; set; }

    [DataMember]
    public int SourceId { get; set; }
}