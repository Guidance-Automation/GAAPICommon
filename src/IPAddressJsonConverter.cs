using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace GAAPICommon;

/// <summary>
/// A custom JSON converter for handling <see cref="IPAddress"/> objects, 
/// allowing for correct serialization and deserialization of IP address data in JSON formats.
/// </summary>
public class IPAddressJsonConverter : JsonConverter<IPAddress>
{
    /// <summary>
    /// Determines if the type can be converted by this converter.
    /// </summary>
    /// <param name="typeToConvert">The type being checked for conversion compatibility.</param>
    /// <returns>Returns true if the type is assignable from <see cref="IPAddress"/>, otherwise false.</returns>
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsAssignableFrom(typeof(IPAddress));
    }

    /// <summary>
    /// Reads and converts the JSON to an <see cref="IPAddress"/>.
    /// </summary>
    /// <param name="reader">The reader to parse JSON content.</param>
    /// <param name="typeToConvert">The type of the object to convert to.</param>
    /// <param name="options">Options to control the behavior during reading.</param>
    /// <returns>The converted <see cref="IPAddress"/> or null if the JSON string is empty or not valid.</returns>
    public override IPAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? token = reader.GetString();

        if (reader.TokenType != JsonTokenType.String || string.IsNullOrEmpty(token))
        {
            return null;
        }

        return IPAddress.Parse(token);
    }

    /// <summary>
    /// Writes an <see cref="IPAddress"/> object to JSON.
    /// </summary>
    /// <param name="writer">The writer to which the JSON is written.</param>
    /// <param name="value">The value to convert and write.</param>
    /// <param name="options">Options to control the behavior during writing.</param>
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