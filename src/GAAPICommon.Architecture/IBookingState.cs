namespace GAAPICommon.Architecture
{
    public interface IBookingState
    {
        int AgentId { get; }

        BookingState BookingState { get; }

        int JobId { get; }

        int TaskId { get; }
    }
}
