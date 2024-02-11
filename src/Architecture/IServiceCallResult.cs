using ProtoBuf;

namespace GAAPICommon.Architecture;

[ProtoContract]
public interface IServiceCallResult
{
    [ProtoMember(1)]
    public string ExceptionMessage { get; }

    [ProtoMember(2)]
    public string ExceptionSource { get; }

    [ProtoMember(3)]
    public string ExceptionStackTrace { get; }

    [ProtoMember(4)]
    public int ServiceCode { get; }
}