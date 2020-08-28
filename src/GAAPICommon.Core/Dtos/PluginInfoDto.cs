namespace GAAPICommon.Core.Dtos
{
    public class PluginInfoDto
    {
        public string AbsFilePath { get; set; } // Abs file path, e.g. "C:\temp\foo.dll"

        public string AssemblyName { get; set; } // Plugin assembly name, e.g. "AwesomePlugin"

        public SemVerDto Version { get; set; } // Plugin version
    }
}