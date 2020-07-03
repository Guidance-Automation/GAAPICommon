using GAAPICommon.Core.Dtos;
using NUnit.Framework;
using System;

namespace GAAPICommon.Core.Test
{
	[TestFixture]
	public class TServiceCallResultDto
	{
		[Test]
		public void ArgumentOutOfRangeException()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() =>
			{
				Exception ex = new Exception("Ohes noes!");

				ServiceCallResultDto dto = new ServiceCallResultDto(0, ex);
			});	
		}
	}
}
