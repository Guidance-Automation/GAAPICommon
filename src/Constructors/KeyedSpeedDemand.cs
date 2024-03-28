namespace GAAPICommon.Constructors;

public class KeyedSpeedDemand
{
    public byte Tick { get; set; } = 0;

    public SpeedDemand? SpeedDemand { get; set; } = null;

    public Guid Guid { get; set; } = Guid.NewGuid();
}