namespace GAAPICommon.Architecture;

public interface IKingpinStatusReporter
{
    public PositionControlStatus PositionControlStatus { get; }

    public NavigationStatus NavigationStatus { get; }

    public DynamicLimiterStatus DynamicLimiterStatus { get; }
}