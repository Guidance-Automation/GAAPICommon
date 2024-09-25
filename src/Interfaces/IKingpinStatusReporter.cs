using GAAPICommon.Enums;

namespace GAAPICommon.Interfaces;

/// <summary>
/// Defines an interface for reporting the status of a kingpin's key operational aspects,
/// including position control, navigation, and dynamic limiter.
/// </summary>
public interface IKingpinStatusReporter
{
    /// <summary>
    /// Gets the status of the position control system of the kingpin,
    /// indicating the overall health and operational state of the positioning functionality.
    /// </summary>
    public PositionControlStatus PositionControlStatus { get; }

    /// <summary>
    /// Gets the status of the navigation system of the kingpin,
    /// reflecting how navigation operations are currently performing or any issues encountered.
    /// </summary>
    public NavigationStatus NavigationStatus { get; }

    /// <summary>
    /// Gets the status from the dynamic limiter system, which includes information on any dynamic
    /// constraints or errors that might affect the kingpin's operation.
    /// </summary>
    public DynamicLimiterStatus DynamicLimiterStatus { get; }
}