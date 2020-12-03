using GAAPICommon.Architecture;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GAAPICommon.Core.Dtos
{    
    public class NodeDto
    {
        [DataMember]
        public int Id { get; set; } = -1;

        [DataMember]
        public string Alias { get; set; } = string.Empty;

        [DataMember]
        public PoseDto Pose { get; set; } = null;

        [DataMember]
        public IEnumerable<ServiceType> Services { get; set; }
    }
}
