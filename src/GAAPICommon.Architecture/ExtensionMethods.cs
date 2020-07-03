using System;

namespace GAAPICommon.Architecture
{
	public static class ExtensionMethods
	{
		public static bool IsSuccessful(this IServiceCallResult result)
		{
			if (result == null) throw new ArgumentNullException("result");

			return result.ServiceCode == (int)ServiceCode.NoError;
		}
	}
}
