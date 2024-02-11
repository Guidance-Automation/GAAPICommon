using GAAPICommon.Architecture;
using System;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos
{
    [DataContract]
    public class ServiceCallResultDto<T> : ServiceCallResultDto, IServiceCallResult<T>
    {
        public ServiceCallResultDto(int serviceCode, T value, Exception caughtException = null)
            : base(serviceCode, caughtException)
        {
            Value = value;
        }

        [DataMember]
        public T Value { get; set; } = default;
    }
}