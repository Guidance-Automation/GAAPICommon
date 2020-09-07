using GAAPICommon.Architecture;
using GAAPICommon.Core.Dtos;
using System;

namespace GAAPICommon.Core
{
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
            if (serviceCode < 10) 
                throw new ArgumentOutOfRangeException("Service code must be >= 10");

            return new ServiceCallResultDto<T>(serviceCode, default, null);
        }

        public static ServiceCallResultDto<T> FromCaughtException(int serviceCode, Exception ex)
        {
            if (serviceCode < 10) 
                throw new ArgumentOutOfRangeException("Service code must be >= 10");

            if (ex == null)
                throw new ArgumentNullException("ex");

            return new ServiceCallResultDto<T>(serviceCode, default, ex);
        }
    }
}