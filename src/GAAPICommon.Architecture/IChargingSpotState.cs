namespace GAAPICommon.Architecture;

public interface IChargingSpotState : ISpotState
{
    public IChargeBookingState? ChargeBooking { get; }
}