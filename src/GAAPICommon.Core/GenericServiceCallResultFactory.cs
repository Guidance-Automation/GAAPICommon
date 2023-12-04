using GAAPICommon.Architecture;
using GAAPICommon.Core.Dtos;

namespace GAAPICommon.Core;

public static class ServiceCallResultFactory<T>
{
    public static ServiceCallResultDto<T> FromNoError(T value)
    {
        return new ServiceCallResultDto<T>((int)ServiceCode.NoError, value, null);
    }

    public static ServiceCallResultDto<T> FromUnknownException(Exception ex)
    {
        return new ServiceCallResultDto<T>((int)ServiceCode.UnknownException, default, ex);
    }

    public static ServiceCallResultDto<T> FromClientException(Exception ex)
    {
        return new ServiceCallResultDto<T>((int)ServiceCode.ClientException, default, ex);
    }

    public static ServiceCallResultDto<T> FromUnknownFailure()
    {
        return new ServiceCallResultDto<T>((int)ServiceCode.UnknownFailure, default, null);
    }

    public static ServiceCallResultDto<T> FromServiceNotConfigured()
    {
        return new ServiceCallResultDto<T>((int)ServiceCode.ServiceNotConfigured, default, null);
    }

    public static ServiceCallResultDto<T> FromError(int serviceCode)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(serviceCode, 10);
        return new ServiceCallResultDto<T>(serviceCode, default, null);
    }

    public static ServiceCallResultDto<T> FromCaughtException(int serviceCode, Exception ex)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(serviceCode, 10);
        ArgumentNullException.ThrowIfNull(ex);
        return new ServiceCallResultDto<T>(serviceCode, default, ex);
    }
}