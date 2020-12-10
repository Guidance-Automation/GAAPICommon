using GAAPICommon.Architecture;
using System.Net;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos
{
    [DataContract]
    public class AgentDto
    {
        [DataMember]
        public AgentLifetimeState AgentLifetimeState { get; set; }

        [DataMember]
        public string Alias { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public IPAddress IPAddress { get; set; }
    }
}
