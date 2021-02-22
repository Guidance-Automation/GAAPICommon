using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAAPICommon.Architecture;
using GAAPICommon.Core.Dtos;
using NUnit.Framework;

namespace GAAPICommon.Core.Test
{
    [TestFixture]
    public class TSpotManagerStateDto
    {
        [Test]
        public void Init()
        {
            SpotManagerStateDto dto = new SpotManagerStateDto();
            Assert.IsFalse(string.IsNullOrEmpty(dto.ToSummaryString()));
        }

        [Test]
        public void NullChargeBooking()
        {
            SpotManagerStateDto dto = new SpotManagerStateDto();

            dto.ChargingSpotStates = new ChargingSpotStateDto[]
            {
                new ChargingSpotStateDto()
                {
                    ChargeBooking = null,
                    IsBooked = false,
                    NodeId = 1
                }
            };

            string summaryString = dto.ToSummaryString();
            Assert.IsFalse(string.IsNullOrEmpty(summaryString));
        }

        [Test]
        public void ChargeBooking()
        {
            SpotManagerStateDto dto = new SpotManagerStateDto();

            dto.ChargingSpotStates = new ChargingSpotStateDto[]
            {
                new ChargingSpotStateDto()
                {
                    ChargeBooking = new ChargeBookingStateDto()
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
            };

            string summaryString = dto.ToSummaryString();
            Assert.IsFalse(string.IsNullOrEmpty(summaryString));
        }

    }
}
