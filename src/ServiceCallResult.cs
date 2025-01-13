namespace GAAPICommon;

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
    /// Create a successful result.
    /// </summary>
    public static ServiceCallResult<T> FromSuccess(T value, string? message = null) =>
        new(true, value, null, message);

    /// <summary>
    /// Create a successful result without a value.
    /// </summary>
    public static ServiceCallResult<T> FromSuccess(string? message = null) =>
        new(true, default, null, message);

    /// <summary>
    /// Create an error result.
    /// </summary>
    public static ServiceCallResult<T> FromError(int errorCode, string message) =>
        new(false, default, errorCode, message);

    /// <summary>
    /// Create an error result from an exception.
    /// </summary>
    public static ServiceCallResult<T> FromException(Exception ex) =>
        new(false, default, ex.HResult, ex.Message);

    /// <summary>
    /// Implicit conversion to directly create a successful response.
    /// </summary>
    /// <param name="value"></param>
    public static implicit operator ServiceCallResult<T>(T value) =>
        FromSuccess(value);

    /// <summary>
    /// Implicit conversion to directly create an exception based response.
    /// </summary>
    /// <param name="ex"></param>
    public static implicit operator ServiceCallResult<T>(Exception ex) =>
        FromException(ex);
}