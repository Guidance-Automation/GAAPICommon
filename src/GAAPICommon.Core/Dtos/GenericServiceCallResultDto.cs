using GAAPICommon.Architecture;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos;

[DataContract]
public class ServiceCallResultDto<T>(int serviceCode, T? value, Exception? caughtException = null) : ServiceCallResultDto(serviceCode, caughtException), IServiceCallResult<T>
{
    [DataMember]
    public T? Value { get; set; } = value;
}