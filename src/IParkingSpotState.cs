namespace GAAPICommon;

public interface IParkingSpotState : ISpotState
{
    public IParkBookingState ParkBooking { get; }
}
