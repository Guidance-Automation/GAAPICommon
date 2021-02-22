namespace GAAPICommon.Architecture
{
    public interface IParkingSpotState : ISpotState
    {
        IParkBookingState ParkBooking { get; }
    }
}
