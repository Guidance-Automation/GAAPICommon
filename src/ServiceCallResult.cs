﻿namespace GAAPICommon;

/// <summary>
/// Used as the response object for clients.
/// </summary>
/// <typeparam name="T">The type to expect back.</typeparam>
public readonly struct ServiceCallResult<T>(bool isSuccess, T? value, int? errorCode = null, string? message = null)
{
    /// <summary>
    /// Flag indicating whether or not the call was successful.
    /// </summary>
    public bool IsSuccess { get; } = isSuccess;

    /// <summary>
    /// If expected, the value returned from the call.
    /// </summary>
    public T? Value { get; } = value;

    /// <summary>
    /// In the case of an error, this value will be populated with a code.
    /// </summary>
    public int? ErrorCode { get; } = errorCode;

    /// <summary>
    /// The success or error details.
    /// </summary>
    public string? Message { get; } = message;

    /// <summary>
    /// Generate a Service Call Result from success.
    /// </summary>
    public static ServiceCallResult<T> FromSuccess(T? value, string? message = null)
    {
        return new ServiceCallResult<T>(true, value, null, message);
    }

    /// <summary>
    /// Generate a Service Call Result from an error.
    /// </summary>
    public static ServiceCallResult<T> FromError(int errorCode, string message)
    {
        return new ServiceCallResult<T>(false, default, errorCode, message);
    }
}