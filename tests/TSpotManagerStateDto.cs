using GAAPICommon.Architecture;
using GAAPICommon.Core.Dtos;
using NUnit.Framework;

namespace GAAPICommon.Core.Test;

[TestFixture]
public class TSpotManagerStateDto
{
    [Test]
    public void Init()
    {
        SpotManagerStateDto dto = new();
        Assert.That(string.IsNullOrEmpty(dto.ToSummaryString()), Is.False);
    }

    [Test]
    public void NullChargeBooking()
    {
        SpotManagerStateDto dto = new()
        {
            ChargingSpotStateDtos = new ChargingSpotStateDto[]
            {
                new()
                {
                    ChargeBookingDto = null,
                    IsBooked = false,
                    NodeId = 1
                }
            }
        };

        string summaryString = dto.ToSummaryString();
        Assert.That(string.IsNullOrEmpty(summaryString), Is.False);
    }

    [Test]
    public void ChargeBooking()
    {
        SpotManagerStateDto dto = new()
        {
            ChargingSpotStateDtos = new ChargingSpotStateDto[]
            {
                new()
                {
                    ChargeBookingDto = new ChargeBookingStateDto()
                    {
                        BookingState = BookingState.AwaitingArrival,
                        AgentId = 1,
                        ChargeType = ChargeType.Immediate,
                        JobId = 2,
                        TaskId =3
                    },
                    IsBooked = false,
                    NodeId = 1
                }
            }
        };

        string summaryString = dto.ToSummaryString();
        Assert.That(string.IsNullOrEmpty(summaryString), Is.False);
    }
}