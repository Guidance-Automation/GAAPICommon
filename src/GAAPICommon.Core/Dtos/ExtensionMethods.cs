using System.Text;

namespace GAAPICommon.Core.Dtos;

public static class ExtensionMethods
{
    public static byte[] ToBytes(this KeyedSpeedDemandDto keyedSpeedDemand)
    {
        ArgumentNullException.ThrowIfNull(keyedSpeedDemand);

        byte[] bytes = new byte[27];

        bytes[0] = keyedSpeedDemand.Tick;
        keyedSpeedDemand.Guid.ToByteArray().CopyTo(bytes, 1);
        keyedSpeedDemand.SpeedDemand?.ToBytes().CopyTo(bytes, 17);

        return bytes;
    }

    public static byte[] ToBytes(this SpeedDemandDto speedDemand)
    {
        ArgumentNullException.ThrowIfNull(speedDemand);

        byte[] bytes = new byte[10];

        speedDemand.IPAddress?.GetAddressBytes().CopyTo(bytes, 0);

        BitConverter.GetBytes(speedDemand.Forward).CopyTo(bytes, 4);
        BitConverter.GetBytes(speedDemand.Angular).CopyTo(bytes, 6);
        BitConverter.GetBytes(speedDemand.Lateral).CopyTo(bytes, 8);

        return bytes;
    }

    public static string ToSummary(this ServiceCodeDefinitionDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        StringBuilder builder = new();

        builder.AppendLine("Service Code Definition:");
        builder.AppendLine($"\tService code: {dto.ServiceCode}");
        builder.AppendLine($"\tMessage: {dto.Message}");
        builder.AppendLine($"\tDescription: {dto.Description}");
        builder.Append($"\tSolution: {dto.Solution}");

        return builder.ToString();
    }
}