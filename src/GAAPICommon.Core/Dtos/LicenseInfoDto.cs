using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GAAPICommon.Core.Dtos
{
	[DataContract]
	public class LicenseInfoDto
	{
		[DataMember]
		public int FleetLimit { get; set; }

		[DataMember]
		public int RentalFleetLimit { get; set; }

		[DataMember]
		public int LicensedVersion { get; set; }

		[DataMember]
		public bool MaintenanceStatus { get; set; }

		[DataMember]
		public DateTime ExpiryDate { get; set; }

		[DataMember]
		public DateTime RentalExpiryDate { get; set; }
	}
}
