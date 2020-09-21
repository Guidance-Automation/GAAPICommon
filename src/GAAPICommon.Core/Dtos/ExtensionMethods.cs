using System;
using System.Text;

namespace GAAPICommon.Core.Dtos
{
    public static class ExtensionMethods
    {
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