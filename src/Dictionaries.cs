using GAAPICommon.Enums;

namespace GAAPICommon;

/// <summary>
/// Shared dictionaries for API operations.
/// </summary>
public static class Dictionaries
{
    /// <summary>
    /// Map the <see cref="RoadmapUploadResultFaultCode"/> to a string for display / logging.
    /// </summary>
    public static Dictionary<RoadmapUploadResultFaultCode, string> RoadmapUploadResultFaultCodeDescriptions { get; } = new()
    {
        { RoadmapUploadResultFaultCode.Success, "Success"},
        { RoadmapUploadResultFaultCode.Unknown, "An unknown failure has occurred."},
        { RoadmapUploadResultFaultCode.DuplicateLocalName, "The Local Name is already in use."},
        { RoadmapUploadResultFaultCode.DuplicateMap, "An identical map is already present on the server" }
    };
}