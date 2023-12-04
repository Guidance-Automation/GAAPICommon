using GAAPICommon.Core.Dtos;
using NUnit.Framework;
using System.Net;

namespace GAAPICommon.Core.Test;

[TestFixture]
public class TSpeedDemmandDto
{
    [TestCase(0, 0, 0)]
    [TestCase(10, 20, 30)]
    [TestCase(10, -20, 30)]
    [TestCase(-10, 20, -30)]
    public void ToBytes(short forward, short angular, short lateral)
    {
        IPAddress ipAddress = IPAddress.Parse("192.168.0.1");
        SpeedDemandDto dto = new(ipAddress, forward, angular, lateral);

        byte[] bytes = dto.ToBytes();
        SpeedDemandDto clone = new(bytes);

        Assert.Multiple(() =>
        {
            Assert.That(ipAddress, Is.EqualTo(dto.IPAddress));
            Assert.That(forward, Is.EqualTo(dto.Forward));
            Assert.That(angular, Is.EqualTo(dto.Angular));
            Assert.That(lateral, Is.EqualTo(dto.Lateral));
            Assert.That(ipAddress, Is.EqualTo(clone.IPAddress));
            Assert.That(forward, Is.EqualTo(clone.Forward));
            Assert.That(angular, Is.EqualTo(clone.Angular));
            Assert.That(lateral, Is.EqualTo(clone.Lateral));
        });
    }
}