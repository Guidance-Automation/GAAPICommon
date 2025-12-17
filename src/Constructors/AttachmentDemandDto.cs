using System.Net;

namespace GAAPICommon.Messages;

/// <summary>
/// Represents an attachment demand to send custom attachment commands to a robot.
/// </summary>
public partial class AttachmentDemandDto
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AttachmentDemandDto"/> class from a byte array.
    /// The byte array must be exactly 8 bytes long, containing the IP address and speed values.
    /// </summary>
    /// <param name="bytes">A byte array where the first 4 bytes represent the IP address,
    /// and the following 4 bytes represent up, down, left, right bits.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the byte array is not exactly 8 bytes long.</exception>
    public AttachmentDemandDto(byte[] bytes)
    {
        if (bytes.Length != 8)
            throw new ArgumentOutOfRangeException(nameof(bytes));

        IPAddress = new IPAddress([.. bytes.Take(4)]).ToString();
        Up = BitConverter.ToBoolean(bytes, 4);
        Down = BitConverter.ToBoolean(bytes, 5);
        Left = BitConverter.ToBoolean(bytes, 6);
        Right = BitConverter.ToBoolean(bytes, 7); 
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AttachmentDemandDto"/> class with specified IP address and attachment demands.
    /// </summary>
    /// <param name="ipAddress">The IP address of the robot.</param>
    /// <param name="up">A custom up signal.</param>
    /// <param name="down">A custom down signal.</param>
    /// <param name="left">A custom left signal.</param>
    /// <param name="right">A custom right signal.</param>
    /// <exception cref="ArgumentNullException">Thrown when the provided IP address is null.</exception>
    public AttachmentDemandDto(IPAddress ipAddress, bool up, bool down, bool left, bool right)
    {
        ArgumentNullException.ThrowIfNull(ipAddress);
        IPAddress = ipAddress.ToString();
        Up = up;
        Down = down;
        Left = left;
        Right = right;
    }
}
