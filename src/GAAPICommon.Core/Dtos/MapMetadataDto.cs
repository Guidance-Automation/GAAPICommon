using System;

namespace GAAPICommon.Core.Dtos
{
    public class MapMetadataDto
    {
        public string Alias { get; set; } = null;

        public string Description { get; set; } = null;

        public Guid Guid { get; set; } = Guid.Empty;
    }
}
