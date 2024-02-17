using GAAPICommon.Enums;

namespace GAAPICommon;

public interface IBookingState
{
    public int AgentId { get; }

    public BookingState BookingState { get; }

    public int JobId { get; }

    public int TaskId { get; }
}