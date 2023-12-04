using System.Net;
using GAAPICommon.Core.Dtos;
using NUnit.Framework;

namespace GAAPICommon.Core.Test;

[TestFixture]
public class TKeyedSpeedDemandDto
{
    [TestCase(66, 10, 20, 30)]
    public void ToBytes(byte tick, short forward, short angular, short lateral)
    {
        IPAddress ipAddress = IPAddress.Parse("192.168.0.1");
        Guid guid = Guid.NewGuid();
        SpeedDemandDto speedDemand = new(ipAddress, forward, angular, lateral);

        KeyedSpeedDemandDto dto = new(tick, guid, speedDemand);

        byte[] bytes = dto.ToBytes();
        KeyedSpeedDemandDto clone = new(bytes);

        Assert.Multiple(() =>
        {
            Assert.That(tick, Is.EqualTo(dto.Tick));
            Assert.That(guid, Is.EqualTo(dto.Guid));
            Assert.That(ipAddress, Is.EqualTo(dto?.SpeedDemand?.IPAddress));
            Assert.That(forward, Is.EqualTo(dto?.SpeedDemand?.Forward));
            Assert.That(angular, Is.EqualTo(dto?.SpeedDemand?.Angular));
            Assert.That(lateral, Is.EqualTo(dto?.SpeedDemand?.Lateral));
            Assert.That(tick, Is.EqualTo(clone.Tick));
            Assert.That(guid, Is.EqualTo(clone.Guid));
            Assert.That(ipAddress, Is.EqualTo(clone?.SpeedDemand?.IPAddress));
            Assert.That(forward, Is.EqualTo(clone?.SpeedDemand?.Forward));
            Assert.That(angular, Is.EqualTo(clone?.SpeedDemand?.Angular));
            Assert.That(lateral, Is.EqualTo(clone?.SpeedDemand?.Lateral));
        });
    }
}
