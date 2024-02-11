using GAAPICommon.Core.Dtos;
using NUnit.Framework;
using System.Net;

namespace GAAPICommon.Core.Test
{
    [TestFixture]
    public class TSpeedDemmandDto
    {
        [TestCase(0,0,0)]
        [TestCase(10,20,30)]
        [TestCase(10, -20, 30)]
        [TestCase(-10, 20, -30)]
        public void ToBytes(short forward, short angular, short lateral)
        {
            IPAddress ipAddress = IPAddress.Parse("192.168.0.1");
            SpeedDemandDto dto = new SpeedDemandDto(ipAddress, forward, angular, lateral);

            byte[] bytes = dto.ToBytes();
            SpeedDemandDto clone = new SpeedDemandDto(bytes);


            Assert.AreEqual(ipAddress, dto.IPAddress);
            Assert.AreEqual(forward, dto.Forward);
            Assert.AreEqual(angular, dto.Angular);
            Assert.AreEqual(lateral, dto.Lateral);

            Assert.AreEqual(ipAddress, clone.IPAddress);
            Assert.AreEqual(forward, clone.Forward);
            Assert.AreEqual(angular, clone.Angular);
            Assert.AreEqual(lateral, clone.Lateral);
        }
    }
}
