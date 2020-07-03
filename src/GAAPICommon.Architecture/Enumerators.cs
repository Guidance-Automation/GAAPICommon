using System.Runtime.Serialization;

namespace GAAPICommon.Architecture
{
    [DataContract]
    public enum ServiceCode
    {
        [EnumMember]
        NoError = 0,

        [EnumMember]
        UnknownFailure = 1,

        [EnumMember]
        UnknownException = 2,

        [EnumMember]
        ServiceNotConfigured = 3
    };
}
