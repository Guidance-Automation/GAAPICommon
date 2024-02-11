using GAAPICommon.Architecture;
using System;

namespace GAAPICommon.Core.Dtos;

public class ServiceCallResultDto : IServiceCallResult
{
    public ServiceCallResultDto(int serviceCode, Exception caughtException = null)
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

    public string ExceptionMessage { get; set; } = null;

    public string ExceptionSource { get; set; } = null;

    public string ExceptionStackTrace { get; set; } = null;

    public int ServiceCode { get; set; } = -1;
}