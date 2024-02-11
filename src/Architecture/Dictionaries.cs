using System.Collections.Generic;

namespace GAAPICommon.Architecture;

public static class Dictionaries
{
    public static Dictionary<RoadmapUploadResultFaultCode, string> RoadmapUploadResultFaultCodeDescriptions { get; } = new()
    {
        { RoadmapUploadResultFaultCode.Success, "Success"},
        { RoadmapUploadResultFaultCode.Unknown, "An unknown failure has occurred."},
        { RoadmapUploadResultFaultCode.DuplicateLocalName, "The Local Name is already in use."},
        { RoadmapUploadResultFaultCode.DuplicateMap, "An identical map is already present on the server" }
    };
}