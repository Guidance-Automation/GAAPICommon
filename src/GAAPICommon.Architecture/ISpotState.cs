namespace GAAPICommon.Architecture
{
    public interface ISpotState
    {
        int NodeId { get; }

        bool IsBooked { get; }
    }
}
