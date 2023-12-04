namespace GAAPICommon.Architecture;

public interface IParkingSpotState : ISpotState
{
    public IParkBookingState? ParkBooking { get; }
}