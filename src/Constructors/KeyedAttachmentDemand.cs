using GAAPICommon.Messages;

namespace GAAPICommon.Constructors;

/// <summary>
/// Represents a time-keyed attachment demand for a robot, including a timestamp, a unique identifier,
/// and detailed speed demands for direct robot control.
/// </summary>
public class KeyedAttachmentDemand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KeyedAttachmentDemand"/> class from a byte array.
    /// The byte array must be exactly 25 bytes long, and it encapsulates a tick, a GUID, and speed demand data.
    /// </summary>
    /// <param name="bytes">A byte array where the first byte is the tick,
    /// the next 16 bytes form a GUID, and the remaining 8 bytes are used to construct a <see cref="AttachmentDemand"/>.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the byte array is not exactly 25 bytes long.</exception>
    public KeyedAttachmentDemand(byte[] bytes)
    {
        if (bytes.Length != 25)
            throw new ArgumentOutOfRangeException(nameof(bytes));

        Tick = bytes[0];
        Guid = new Guid([.. bytes.Skip(1).Take(16)]);
        AttachmentDemand = new AttachmentDemandDto([.. bytes.Skip(17).Take(8)]);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KeyedAttachmentDemand"/> class with a specified tick, GUID, and <see cref="AttachmentDemandDto"/>.
    /// </summary>
    /// <param name="tick">The tick value representing a snapshot time or sequence in the control loop.</param>
    /// <param name="guid">A unique identifier for this specific demand instance.</param>
    /// <param name="attachmentDemand">An instance of <see cref="AttachmentDemandDto"/> specifying the demand for the robot.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the GUID is empty.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the provided <see cref="AttachmentDemand"/> is null.</exception>
    public KeyedAttachmentDemand(byte tick, Guid guid, AttachmentDemandDto attachmentDemand)
    {
        if (guid.Equals(Guid.Empty))
            throw new ArgumentOutOfRangeException(nameof(guid));
        Tick = tick;
        Guid = guid;
        AttachmentDemand = attachmentDemand ?? throw new ArgumentNullException(nameof(attachmentDemand));
    }

    /// <summary>
    /// Gets or sets the tick value, representing a snapshot time or sequence in the control loop.
    /// Default is 0.
    /// </summary>
    public byte Tick { get; set; } = 0;

    /// <summary>
    /// Gets or sets the instance of <see cref="SpeedDemandDto"/> that defines the specific speeds for the robot.
    /// </summary>
    public AttachmentDemandDto? AttachmentDemand { get; set; } = null;

    /// <summary>
    /// Gets or sets the GUID, a unique identifier for this specific demand instance.
    /// By default, it is set to a new GUID upon object instantiation.
    /// </summary>
    public Guid Guid { get; set; } = Guid.NewGuid();
}
