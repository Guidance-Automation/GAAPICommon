using ProtoBuf;

namespace GAAPICommon.Core.Dtos;

public class ModuleInfoDto
{
    [ProtoMember(1)]
    public string DisplayName { get; set; } = string.Empty;
}