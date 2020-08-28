namespace GAAPICommon.Architecture
{
    public interface IKingpinStatusReporter
    {
        PositionControlStatus PositionControlStatus { get; }

        NavigationStatus NavigationStatus { get; }

        DynamicLimiterStatus DynamicLimiterStatus { get; }
    }
}