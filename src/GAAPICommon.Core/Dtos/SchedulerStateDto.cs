namespace GAAPICommon.Core.Dtos
{
    public class SchedulerStateDto
    {
        public bool IsInAttractMode { get; set; } = false;

        public bool IsSpotManagementEnabled { get; set; } = false;

        public bool IsUpdateLoopPluginsEnabled { get; set; } = false;

        public MapMetadataDto MapMetadata { get; set; } = null;

        public SpotManagerStateDto SpotManagerState { get; set; } = null;
    }
}
