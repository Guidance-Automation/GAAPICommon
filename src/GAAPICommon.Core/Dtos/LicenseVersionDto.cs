using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GAAPICommon.Core.Dtos
{
	[DataContract]
	public class LicenseVersionDto
	{
		[DataMember]
		public int MajorVersion { get; set; }

		[DataMember]
		public int MinorVersion { get; set; }
	}
}
