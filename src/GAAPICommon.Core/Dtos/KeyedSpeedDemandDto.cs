using System;
using System.Linq;

namespace GAAPICommon.Core.Dtos
{
    public class KeyedSpeedDemandDto
    {
        public KeyedSpeedDemandDto(byte[] bytes)
        {
            if (bytes.Length != 27)
                throw new ArgumentOutOfRangeException("bytes");

            Tick = bytes[0];
            Guid = new Guid(bytes.Skip(1).Take(16).ToArray());
            SpeedDemand = new SpeedDemandDto(bytes.Skip(17).Take(10).ToArray());                 
        }

        public KeyedSpeedDemandDto(byte tick, Guid guid, SpeedDemandDto speedDemand)
        {
            if (guid.Equals(Guid.Empty))
                throw new ArgumentOutOfRangeException("guid");

            Tick = tick;
            Guid = guid;
            SpeedDemand = speedDemand ?? throw new ArgumentNullException("speedDemand");
        }

        public byte Tick { get; set; } = 0;

        public SpeedDemandDto SpeedDemand { get; set; } = null;

        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}
