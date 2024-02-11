namespace GAAPICommon.Core.Dtos
{
    /// <summary>
    /// Includes the spot manager config as a sub-item
    /// </summary>
    public class SchedulerConfigDto
    {
        public bool IsInAttractMode { get; set; } = false;

        public bool IsSpotManagementEnabled { get; set; } = false;

        public bool IsUpdateLoopPluginsEnabled { get; set; } = false;

        public MapMetadataDto MapMetadata { get; set; } = null;

        public SpotManagerConfigDto SpotManagerConfig { get; set; } = null;
    }
}
