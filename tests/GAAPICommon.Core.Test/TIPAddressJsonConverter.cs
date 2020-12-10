using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using NUnit.Framework;
using NUnit;

namespace GAAPICommon.Core.Test
{
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

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new IPAddressJsonConverter());
            settings.Formatting = Formatting.Indented;

            string json = JsonConvert.SerializeObject(expected, settings);
            Assert.IsNotNull(json);

            IEnumerable<IPAddress> actual = JsonConvert.DeserializeObject<IEnumerable<IPAddress>>(json, settings);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("127.0.0.1")]
        [TestCase("192.168.0.1")]
        public void IPAddressJson(string ipV4string)
        {
            IPAddress expected = IPAddress.Parse(ipV4string);
 
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new IPAddressJsonConverter());
            settings.Formatting = Formatting.Indented;

            string json = JsonConvert.SerializeObject(expected, settings);

            Assert.IsNotNull(json);

            IPAddress actual = JsonConvert.DeserializeObject<IPAddress>(json, settings);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("127.0.0.1")]
        [TestCase("192.168.0.1")]
        public void FooIPAddressableJson(string ipV4string)
        {
            FooIPAddressable foo = new FooIPAddressable()
            {
                Alias = ipV4string,
                Id = 69,
                IPAddress = IPAddress.Parse(ipV4string)
            };

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new IPAddressJsonConverter());
            settings.Formatting = Formatting.Indented;

            string json = JsonConvert.SerializeObject(foo, settings);

            Assert.IsNotNull(json);

            FooIPAddressable deSerialized = JsonConvert.DeserializeObject<FooIPAddressable>(json, settings);

            Assert.AreEqual(foo.Alias, deSerialized.Alias);
            Assert.AreEqual(foo.Id, deSerialized.Id);
            Assert.AreEqual(deSerialized.IPAddress, foo.IPAddress);

        }
    }
}
