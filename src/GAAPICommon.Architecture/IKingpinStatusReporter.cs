using System;
using System.Collections.Generic;
using System.Text;

namespace GAAPICommon.Architecture
{
	public interface IKingpinStatusReporter
	{
		PositionControlStatus PositionControlStatus { get; }

		NavigationStatus NavigationStatus { get; }

		DynamicLimiterStatus DynamicLimiterStatus { get; }
	}
}
