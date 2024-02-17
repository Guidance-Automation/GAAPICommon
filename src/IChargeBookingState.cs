using GAAPICommon.Enums;

namespace GAAPICommon;

public interface IChargeBookingState : IBookingState
{
    public ChargeType ChargeType { get; }
}