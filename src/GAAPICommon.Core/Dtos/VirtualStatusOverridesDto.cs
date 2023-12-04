using GAAPICommon.Architecture;

namespace GAAPICommon.Core.Dtos;

public class VirtualStatusOverridesDto
{
    public NavigationStatus? NavigationStatusOverride { get; set; } = null;

    public DynamicLimiterStatus? DynamicLimiterStatusOverride { get; set; } = null;

    public PositionControlStatus? PositionControlStatusOverride { get; set; } = null;
}