using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GAAPICommon.Core.Dtos;
using NUnit.Framework;

namespace GAAPICommon.Core.Test
{
    [TestFixture]
    public class TKeyedSpeedDemandDto
    {
        [TestCase(66, 10, 20, 30)]
        public void ToBytes(byte tick, short forward, short angular, short lateral)
        {
            IPAddress ipAddress = IPAddress.Parse("192.168.0.1");
            Guid guid = Guid.NewGuid();
            SpeedDemandDto speedDemand = new SpeedDemandDto(ipAddress, forward, angular, lateral);

            KeyedSpeedDemandDto dto = new KeyedSpeedDemandDto(tick, guid, speedDemand);

            byte[] bytes = dto.ToBytes();
            KeyedSpeedDemandDto clone = new KeyedSpeedDemandDto(bytes);

            Assert.AreEqual(tick, dto.Tick);
            Assert.AreEqual(guid, dto.Guid);
            Assert.AreEqual(ipAddress, dto.SpeedDemand.IPAddress);
            Assert.AreEqual(forward, dto.SpeedDemand.Forward);
            Assert.AreEqual(angular, dto.SpeedDemand.Angular);
            Assert.AreEqual(lateral, dto.SpeedDemand.Lateral);

            Assert.AreEqual(tick, clone.Tick);
            Assert.AreEqual(guid, clone.Guid);
            Assert.AreEqual(ipAddress, clone.SpeedDemand.IPAddress);
            Assert.AreEqual(forward, clone.SpeedDemand.Forward);
            Assert.AreEqual(angular, clone.SpeedDemand.Angular);
            Assert.AreEqual(lateral, clone.SpeedDemand.Lateral);
        }
    }
}
