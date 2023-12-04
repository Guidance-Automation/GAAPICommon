﻿using GAAPICommon.Architecture;
using GAAPICommon.Core.Dtos;
using System;

namespace GAAPICommon.Core
{
    public static class ServiceCallResultFactory
    {
        public static ServiceCallResultDto FromNoError()
        {
            return new ServiceCallResultDto((int)ServiceCode.NoError, null);
        }

        public static ServiceCallResultDto FromClientException(Exception ex)
        {
            return new ServiceCallResultDto((int)ServiceCode.ClientException, ex);
        }

        public static ServiceCallResultDto FromUnknownException(Exception ex)
        {
            return new ServiceCallResultDto((int)ServiceCode.UnknownException, ex);
        }

        public static ServiceCallResultDto FromUnknownFailure()
        {
            return new ServiceCallResultDto((int)ServiceCode.UnknownFailure, null);
        }

        public static ServiceCallResultDto FromServiceNotConfigured()
        {
            return new ServiceCallResultDto((int)ServiceCode.ServiceNotConfigured, null);
        }

        public static ServiceCallResultDto FromError(int serviceCode)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(serviceCode, 10);
            return new ServiceCallResultDto(serviceCode, null);
        }

        public static ServiceCallResultDto FromCaughtException(int serviceCode, Exception ex)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(serviceCode, 10);
            ArgumentNullException.ThrowIfNull(ex);
            return new ServiceCallResultDto(serviceCode, ex);
        }
    }
}