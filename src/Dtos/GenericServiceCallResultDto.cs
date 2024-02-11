using GAAPICommon.Architecture;
using ProtoBuf;
using System;

namespace GAAPICommon.Core.Dtos;

[ProtoContract]
public class ServiceCallResultDto<T> : ServiceCallResultDto, IServiceCallResult<T>
{
    public ServiceCallResultDto() : base(0, null)
    {
        
    }

    public ServiceCallResultDto(int serviceCode, T value, Exception caughtException = null)
        : base(serviceCode, caughtException)
    {
        Value = value;
    }

    [ProtoMember(1)]
    public T Value { get; set; } = default;
}