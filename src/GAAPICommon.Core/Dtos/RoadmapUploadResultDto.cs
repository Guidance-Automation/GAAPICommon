namespace GAAPICommon.Core.Dtos
{
    public class RoadmapUploadResultDto
    {
        public int Id { get; set; } = -1;

        public string LocalName { get; set; } = null;

        public bool Success { get { return Id >= 0; } }
    }
}
