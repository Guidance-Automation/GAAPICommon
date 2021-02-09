using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos
{    
    public class ModuleInfoDto
    {
        [DataMember]
        public string DisplayName { get; set; } = string.Empty;
    }
}
