using GAAPICommon.Messages;

namespace GAAPICommon.Constructors;

/// <summary>
/// Represents a time-keyed speed demand for a robot, including a timestamp, a unique identifier,
/// and detailed speed demands for direct robot control.
/// </summary>
public class KeyedSpeedDemand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KeyedSpeedDemand"/> class from a byte array.
    /// The byte array must be exactly 27 bytes long, and it encapsulates a tick, a GUID, and speed demand data.
    /// </summary>
    /// <param name="bytes">A byte array where the first byte is the tick,
    /// the next 16 bytes form a GUID, and the remaining 10 bytes are used to construct a <see cref="SpeedDemand"/>.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the byte array is not exactly 27 bytes long.</exception>
    public KeyedSpeedDemand(byte[] bytes)
    {
        if (bytes.Length != 27)
            throw new ArgumentOutOfRangeException(nameof(bytes));

        Tick = bytes[0];
        Guid = new Guid(bytes.Skip(1).Take(16).ToArray());
        SpeedDemand = new SpeedDemandDto(bytes.Skip(17).Take(10).ToArray());
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KeyedSpeedDemand"/> class with a specified tick, GUID, and <see cref="SpeedDemandDto"/>.
    /// </summary>
    /// <param name="tick">The tick value representing a snapshot time or sequence in the control loop.</param>
    /// <param name="guid">A unique identifier for this specific demand instance.</param>
    /// <param name="speedDemand">An instance of <see cref="SpeedDemandDto"/> specifying the speeds for the robot.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the GUID is empty.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the provided <see cref="SpeedDemand"/> is null.</exception>
    public KeyedSpeedDemand(byte tick, Guid guid, SpeedDemandDto speedDemand)
    {
        if (guid.Equals(Guid.Empty))
            throw new ArgumentOutOfRangeException(nameof(guid));
        Tick = tick;
        Guid = guid;
        SpeedDemand = speedDemand ?? throw new ArgumentNullException(nameof(speedDemand));
    }

    /// <summary>
    /// Gets or sets the tick value, representing a snapshot time or sequence in the control loop.
    /// Default is 0.
    /// </summary>
    public byte Tick { get; set; } = 0;

    /// <summary>
    /// Gets or sets the instance of <see cref="SpeedDemandDto"/> that defines the specific speeds for the robot.
    /// </summary>
    public SpeedDemandDto? SpeedDemand { get; set; } = null;

    /// <summary>
    /// Gets or sets the GUID, a unique identifier for this specific demand instance.
    /// By default, it is set to a new GUID upon object instantiation.
    /// </summary>
    public Guid Guid { get; set; } = Guid.NewGuid();
}