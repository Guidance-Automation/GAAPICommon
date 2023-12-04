using GAAPICommon.Architecture;
using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos;

[DataContract]
public class ServiceCallResultDto : IServiceCallResult
{
    public ServiceCallResultDto()
    {
    }

    public ServiceCallResultDto(int serviceCode, Exception? caughtException = null)
    {
        if (serviceCode == 0 && caughtException != null)
            throw new ArgumentOutOfRangeException(nameof(caughtException), "Caught exception cannot be populated with a service code value of 0");

        ServiceCode = serviceCode;

        if (caughtException != null)
        {
            ExceptionMessage = caughtException.Message;
            ExceptionSource = caughtException.Source;
            ExceptionStackTrace = caughtException.StackTrace;
        }
    }

    [DataMember]
    public string? ExceptionMessage { get; set; }

    [DataMember]
    public string? ExceptionSource { get; set; }

    [DataMember]
    public string? ExceptionStackTrace { get; set; }

    [DataMember]
    public int ServiceCode { get; set; } = -1;
}