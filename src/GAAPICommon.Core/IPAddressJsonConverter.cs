using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace GAAPICommon.Core
{
    public class IPAddressJsonConverter : JsonConverter<IPAddress>
    {
        public override IPAddress ReadJson(JsonReader reader, Type objectType, IPAddress existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (objectType == typeof(IPAddress))
                return IPAddress.Parse(JToken.Load(reader).ToString());           

            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, IPAddress value, JsonSerializer serializer)
        {
            if (value.GetType() == typeof(IPAddress))
            {
                JToken.FromObject(value.ToString()).WriteTo(writer);
                return;
            }

            throw new NotImplementedException();
        }
    }
}
