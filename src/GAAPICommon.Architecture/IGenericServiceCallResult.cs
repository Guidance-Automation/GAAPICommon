namespace GAAPICommon.Architecture;

public interface IServiceCallResult<T> : IServiceCallResult
{
    public T? Value { get; }
}