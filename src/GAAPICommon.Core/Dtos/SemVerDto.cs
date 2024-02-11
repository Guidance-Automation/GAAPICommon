using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos
{
    /// <summary>
    /// This goes out on WCF so needs DataContract and DataMember attributes.
    /// </summary>
    [DataContract]
    public class SemVerDto
    {
        [DataMember]
        public int Major { get; set; } = -1;

        [DataMember]
        public int Minor { get; set; } = -1;

        [DataMember]
        public int Patch { get; set; } = -1;

        [DataMember]
        public string ReleaseFlag { get; set; } = string.Empty;
    }
}