using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos;

[DataContract]
public class SpotManagerConfigDto
{
    [DataMember]
    public TimeSpan IdleTimeout { get; set; } = TimeSpan.MinValue;

    [DataMember]
    public double ChargeDowngradeLevel { get; set; } = double.NaN;

    [DataMember]
    public double ImmediateChargeLevel { get; set; } = double.NaN;
}