namespace GAAPICommon.Architecture;

public interface IChargeBookingState : IBookingState
{
    public ChargeType ChargeType { get; }
}