using GAAPICommon.Architecture;

namespace GAAPICommon.Core.Dtos;

public class RoadmapUploadResultDto
{
    public int Id { get; set; } = -1;

    public string? LocalName { get; set; }

    public RoadmapUploadResultFaultCode FaultCode { get; set; }

    /// <summary>
    /// This can be one of two things:
    ///     If the local name is identical, then this can be a map with the same local name but different content.
    ///     Or the map can be identical equal (identical content) to a different named map on the server.
    /// </summary>
    public MapCardDto? ConflictingMapCard { get; set; } 

    public bool Success =>  FaultCode == RoadmapUploadResultFaultCode.Success; 
}