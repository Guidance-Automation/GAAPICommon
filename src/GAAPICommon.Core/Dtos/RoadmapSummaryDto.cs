using System;
using System.Collections.Generic;
using System.Text;

namespace GAAPICommon.Core.Dtos
{
    public class RoadmapSummaryDto
    {
        public int Id { get; set; } = -1;

        public string Alias { get; set; } = string.Empty;

        public bool IsSelected { get; set; } = false;
    }
}
