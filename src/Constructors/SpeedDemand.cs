using System.Net;

namespace GAAPICommon.Constructors;

/// <summary>
/// Represents a speed demand to control a robot's motion, encapsulating details about
/// direction and magnitude of movement in various axes.
/// </summary>
public class SpeedDemand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SpeedDemand"/> class from a byte array.
    /// The byte array must be exactly 10 bytes long, containing the IP address and speed values.
    /// </summary>
    /// <param name="bytes">A byte array where the first 4 bytes represent the IP address,
    /// and the following 6 bytes represent the forward, angular, and lateral speeds as short integers.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the byte array is not exactly 10 bytes long.</exception>
    public SpeedDemand(byte[] bytes)
    {
        if (bytes.Length != 10)
            throw new ArgumentOutOfRangeException(nameof(bytes));

        IPAddress = new IPAddress(bytes.Take(4).ToArray());
        Forward = BitConverter.ToInt16(bytes, 4);
        Angular = BitConverter.ToInt16(bytes, 6);
        Lateral = BitConverter.ToInt16(bytes, 8);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SpeedDemand"/> class with specified IP address and movement speeds.
    /// </summary>
    /// <param name="ipAddress">The IP address of the robot.</param>
    /// <param name="forward">Forward speed value.</param>
    /// <param name="angular">Angular speed value for rotational movement.</param>
    /// <param name="lateral">Lateral speed value for side-to-side movement.</param>
    /// <exception cref="ArgumentNullException">Thrown when the provided IP address is null.</exception>
    public SpeedDemand(IPAddress ipAddress, short forward, short angular, short lateral)
    {
        ArgumentNullException.ThrowIfNull(ipAddress);
        IPAddress = ipAddress;
        Forward = forward;
        Angular = angular;
        Lateral = lateral;
    }

    /// <summary>
    /// Gets or sets the IP address of the robot. Null if not specified.
    /// </summary>
    public IPAddress? IPAddress { get; set; } = null;

    /// <summary>
    /// Gets or sets the forward speed value. Default is 0.
    /// </summary>
    public short Forward { get; set; } = 0;

    /// <summary>
    /// Gets or sets the angular speed value, which controls rotational movement. Default is 0.
    /// </summary>
    public short Angular { get; set; } = 0;

    /// <summary>
    /// Gets or sets the lateral speed value, which controls side-to-side movement. Default is 0.
    /// </summary>
    public short Lateral { get; set; } = 0;
}