using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace GAAPICommon;

public class IPAddressJsonConverter : JsonConverter<IPAddress>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsAssignableFrom(typeof(IPAddress));
    }

    public override IPAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? token = reader.GetString();

        if (reader.TokenType != JsonTokenType.String || string.IsNullOrEmpty(token))
        {
            return null;
        }

        return IPAddress.Parse(token);
    }

    public override void Write(Utf8JsonWriter writer, IPAddress value, JsonSerializerOptions options)
    {
        string ipAddressString = string.Empty;

        if (value != null)
        {
            ipAddressString = value.ToString();
        }

        writer.WriteStringValue(ipAddressString);
    }
}