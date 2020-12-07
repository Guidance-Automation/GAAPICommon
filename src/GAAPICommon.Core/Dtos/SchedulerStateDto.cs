using System;
using System.Collections.Generic;
using System.Text;

namespace GAAPICommon.Core.Dtos
{
    public class SchedulerStateDto
    {
        bool IsInAttractMode { get; set; }

        bool IsSpotManagementEnabled { get; set; }

        bool IsUpdateLoopPluginsEnabled { get; set; }
    }
}
