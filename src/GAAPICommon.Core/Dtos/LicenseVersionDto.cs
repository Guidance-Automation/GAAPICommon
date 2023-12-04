using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos;

[DataContract]
public class LicenseVersionDto
{
    [DataMember]
    public int MajorVersion { get; set; }

    [DataMember]
    public int MinorVersion { get; set; }
}