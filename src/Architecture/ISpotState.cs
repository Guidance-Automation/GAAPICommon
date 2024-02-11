namespace GAAPICommon.Architecture;

public interface ISpotState
{
    public int NodeId { get; }

    public bool IsBooked { get; }
}