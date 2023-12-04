namespace GAAPICommon.Core.Dtos;

public class MapCardDto
{
    public int Id { get; set; } = -1;

    public string? LocalName { get; set; }

    public MapMetadataDto? MapMetadata { get; set; }

    public DateTime LastAccess { get; set; } = DateTime.MinValue;

    public bool IsSelected { get; set; } = false;
}
