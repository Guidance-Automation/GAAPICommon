using System;

namespace GAAPICommon.Core.Dtos
{
    /// <summary>
    /// Includes the spot manager state as a sub-item
    /// </summary>
    public class SchedulerStateDto
    {
        public Guid InstanceId { get; set; }

        public byte Cycle { get; set; }

        public TimeSpan UpTime { get; set; }

        public SpotManagerStateDto SpotManagerState { get; set; }
    }
}
