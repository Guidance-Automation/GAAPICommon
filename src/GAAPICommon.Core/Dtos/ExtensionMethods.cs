using System;
using System.Text;

namespace GAAPICommon.Core.Dtos
{
    public static class ExtensionMethods
    {
        public static byte[] ToBytes(this SpeedDemandDto speedDemand)
        {
            if (speedDemand == null)
                throw new ArgumentNullException("speedDemmand");


            byte[] bytes = new byte[10];

            speedDemand.IPAddress.GetAddressBytes().CopyTo(bytes, 0);

            BitConverter.GetBytes(speedDemand.Forward).CopyTo(bytes, 4);
            BitConverter.GetBytes(speedDemand.Angular).CopyTo(bytes, 6);
            BitConverter.GetBytes(speedDemand.Lateral).CopyTo(bytes, 8);

            return bytes;
        }

        public static string ToSummary(this ServiceCodeDefinitionDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException("dto");

            StringBuilder builder = new StringBuilder();

            builder.AppendLine("Service Code Definition:");
            builder.AppendLine($"\tService code: {dto.ServiceCode}");
            builder.AppendLine($"\tMessage: {dto.Message}");
            builder.AppendLine($"\tDescription: {dto.Description}");
            builder.Append($"\tSolution: {dto.Solution}");

            return builder.ToString();
        }
    }
}