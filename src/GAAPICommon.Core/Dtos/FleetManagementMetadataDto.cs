using System;
using System.Collections.Generic;
using System.Text;

namespace GAAPICommon.Core.Dtos
{
    public class FleetManagementMetadataDto
    {
        public string ProductName { get; set; }

        public SemVerDto SemVer { get; set; }
    }
}
