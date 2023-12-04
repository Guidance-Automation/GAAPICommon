namespace GAAPICommon.Architecture;

public interface IServiceCallResult
{
    public string? ExceptionMessage { get; }

    public string? ExceptionSource { get; }

    public string? ExceptionStackTrace { get; }

    public int ServiceCode { get; }
}