namespace GAAPICommon.Architecture
{
	public interface IServiceCallResult<T> : IServiceCallResult
	{
		T Value { get; }
	}
}
