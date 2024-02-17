namespace GAAPICommon;

public interface IChargingSpotState : ISpotState
{
    public IChargeBookingState ChargeBooking { get; }
}
