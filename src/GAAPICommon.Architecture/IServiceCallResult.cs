namespace GAAPICommon.Architecture
{
	public interface IServiceCallResult
	{
        string ExceptionMessage { get; } 

        string ExceptionSource { get; }
        
        string ExceptionStackTrace { get; }

        int ServiceCode { get; }
    }
}
