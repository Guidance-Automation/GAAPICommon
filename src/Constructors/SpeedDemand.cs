using System.Net;

namespace GAAPICommon.Constructors;
public class SpeedDemand
{
    public IPAddress? IPAddress { get; set; } = null;

    public short Forward { get; set; } = 0;

    public short Angular { get; set; } = 0;

    public short Lateral { get; set; } = 0;
}