using GAAPICommon.Architecture;
using ProtoBuf;
using System.Net;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GAAPICommon.Core.Dtos;

[ProtoContract]
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

[ProtoContract]
public class AgentDto
{
    [ProtoMember(1)]
    public AgentLifetimeState AgentLifetimeState { get; set; }

    [ProtoMember(2)]
    public string Alias { get; set; }

    [ProtoMember(3)]
    public int Id { get; set; }

    [ProtoMember(4)]
    [JsonConverter(typeof(IPAddressJsonConverter))]
    public IPAddress IPAddress { get; set; }

    [ProtoMember(5)]
    public AgentMarkovState MarkovState { get; set; }

    [ProtoMember(6)]
    public int CurrentJobId { get; set; }

    [ProtoMember(7)]
    public int CurrentNodeId { get; set; }
}