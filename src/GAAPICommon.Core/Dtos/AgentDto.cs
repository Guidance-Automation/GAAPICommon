using GAAPICommon.Architecture;
using Newtonsoft.Json;
using System.Net;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos
{
    [DataContract]
    public enum AgentMarkovState
    {
	[EnumMember]
	Moving = 2,

	[EnumMember]
	Error = 5,

	[EnumMember]
	Fatal = 6,

	[EnumMember]
	Servicing = 4,

	[EnumMember]
	Ready = 1,

	[EnumMember]
	Sleeping = 3,

	[EnumMember]
	Waiting = 0,

	[EnumMember]
	Stationary = 7,

	[EnumMember]
	Executing = 8,

	[EnumMember]
	Aborting = 9
    };

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
        [JsonConverter(typeof(IPAddressJsonConverter))]
        public IPAddress IPAddress { get; set; }

	[DataMember]
	public AgentMarkovState MarkovState { get; set; }

	[DataMember]
	public int CurrentJobId { get; set; }

	[DataMember]
	public int CurrentNodeId { get; set; }
    }
}
