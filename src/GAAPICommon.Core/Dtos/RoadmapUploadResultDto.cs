using GAAPICommon.Architecture;

namespace GAAPICommon.Core.Dtos
{
    public class RoadmapUploadResultDto
    {
        public int Id { get; set; } = -1;

        public string LocalName { get; set; } = null;

        public RoadmapUploadResultFaultCode FaultCode { get; set; }

        public bool Success =>  FaultCode == RoadmapUploadResultFaultCode.Success; 
    }
}
