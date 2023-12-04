namespace GAAPICommon.Core.Dtos;

public class MapMetadataDto
{
    public string? Alias { get; set; }

    public string? Description { get; set; }

    public Guid Guid { get; set; } = Guid.Empty;
}