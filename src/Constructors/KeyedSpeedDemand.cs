namespace GAAPICommon.Constructors;

public class KeyedSpeedDemand
{
    public KeyedSpeedDemand(byte[] bytes)
    {
        if (bytes.Length != 27)
            throw new ArgumentOutOfRangeException(nameof(bytes));

        Tick = bytes[0];
        Guid = new Guid(bytes.Skip(1).Take(16).ToArray());
        SpeedDemand = new SpeedDemand(bytes.Skip(17).Take(10).ToArray());
    }

    public KeyedSpeedDemand(byte tick, Guid guid, SpeedDemand speedDemand)
    {
        if (guid.Equals(Guid.Empty))
            throw new ArgumentOutOfRangeException(nameof(guid));

        Tick = tick;
        Guid = guid;
        SpeedDemand = speedDemand ?? throw new ArgumentNullException(nameof(speedDemand));
    }

    public byte Tick { get; set; } = 0;

    public SpeedDemand? SpeedDemand { get; set; } = null;

    public Guid Guid { get; set; } = Guid.NewGuid();
}