namespace GAAPICommon.Core.Dtos
{
    public class SchedulerStateDto
    {
        public bool IsInAttractMode { get; set; }

        public bool IsSpotManagementEnabled { get; set; }

        public bool IsUpdateLoopPluginsEnabled { get; set; }

        public MapMetadataDto MapMetadata { get; set; }
    }
}
