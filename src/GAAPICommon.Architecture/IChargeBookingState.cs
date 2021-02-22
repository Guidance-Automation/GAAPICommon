namespace GAAPICommon.Architecture
{
    public interface IChargeBookingState : IBookingState
    {
        ChargeType ChargeType { get; }
    }
}
