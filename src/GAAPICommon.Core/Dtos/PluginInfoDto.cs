namespace GAAPICommon.Core.Dtos;

public class PluginInfoDto
{
    /// <summary>
    /// Abs file path, e.g. "C:\temp\foo.dll"
    /// </summary>
    public string? AbsFilePath { get; set; }

    /// <summary>
    /// Plugin assembly name, e.g. "AwesomePlugin"
    /// </summary>
    public string? AssemblyName { get; set; }

    /// <summary>
    /// Plugin version
    /// </summary>
    public SemVerDto? Version { get; set; }
}