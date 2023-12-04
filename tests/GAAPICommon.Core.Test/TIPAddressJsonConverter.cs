using System.Net;
using System.Text.Json;
using NUnit.Framework;

namespace GAAPICommon.Core.Test;

[TestFixture]
public class TIPAddressJsonConverter
{
    [Test]
    public void IEnumerableIPAddress()
    {
        IEnumerable<IPAddress> expected = new IPAddress[]
        {
            IPAddress.Loopback,
            IPAddress.Parse("192.168.0.1"),
            IPAddress.Parse("192.168.0.69")
        };

        JsonSerializerOptions settings = new();
        settings.Converters.Add(new IPAddressJsonConverter());
        settings.WriteIndented = true;

        
        string json = JsonSerializer.Serialize(expected, settings);
        Assert.That(json, Is.Not.EqualTo(null));

        IEnumerable<IPAddress>? actual = JsonSerializer.Deserialize<IEnumerable<IPAddress>>(json, settings);
        Assert.That(actual, Is.EquivalentTo(expected));
    }

    [TestCase("127.0.0.1")]
    [TestCase("192.168.0.1")]
    public void IPAddressJson(string ipV4string)
    {
        IPAddress expected = IPAddress.Parse(ipV4string);

        JsonSerializerOptions settings = new();
        settings.Converters.Add(new IPAddressJsonConverter());
        settings.WriteIndented = true;

        string json = JsonSerializer.Serialize(expected, settings);

        Assert.That(json, Is.Not.EqualTo(null));

        IPAddress? actual = JsonSerializer.Deserialize<IPAddress>(json, settings);
        Assert.That(expected, Is.EqualTo(actual));
    }

    [TestCase("127.0.0.1")]
    [TestCase("192.168.0.1")]
    public void FooIPAddressableJson(string ipV4string)
    {
        FooIPAddressable foo = new()
        {
            Alias = ipV4string,
            Id = 69,
            IPAddress = IPAddress.Parse(ipV4string)
        };

        JsonSerializerOptions settings = new();
        settings.Converters.Add(new IPAddressJsonConverter());
        settings.WriteIndented = true;

        string json = JsonSerializer.Serialize(foo, settings);

        Assert.That(json, Is.Not.EqualTo(null));

        FooIPAddressable? deSerialized = JsonSerializer.Deserialize<FooIPAddressable>(json, settings);

        Assert.Multiple(() =>
        {
            Assert.That(deSerialized, Is.Not.EqualTo(null));
            Assert.That(foo.Alias, Is.EqualTo(deSerialized?.Alias));
            Assert.That(foo.Id, Is.EqualTo(deSerialized?.Id));
        });
        Assert.That(deSerialized.IPAddress, Is.EqualTo(foo.IPAddress));
    }
}