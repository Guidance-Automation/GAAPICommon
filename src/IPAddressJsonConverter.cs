using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GAAPICommon.Core;

public class IPAddressJsonConverter : JsonConverter<IPAddress>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsAssignableFrom(typeof(IPAddress));
    }

    public override IPAddress Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var token = reader.GetString();

        if (reader.TokenType != JsonTokenType.String || string.IsNullOrEmpty(token))
        {
            return null;
        }

        return IPAddress.Parse(token);
    }

    public override void Write(Utf8JsonWriter writer, IPAddress value, JsonSerializerOptions options)
    {
        var ipAddressString = string.Empty;

        if (value != null)
        {
            ipAddressString = value.ToString();
        }

        writer.WriteStringValue(ipAddressString);
    }
}