using System;

namespace GAAPICommon.Core.Dtos
{
    public class MapCardDto
    {
        public int Id { get; set; } = -1;

        public string LocalName { get; set; } = null;

        public string Alias { get; set; } = null;

        public string Description { get; set; } = null;

        public DateTime LastAccess { get; set; } = DateTime.MinValue;

        public bool IsSelected { get; set; } = false;
    }
}
