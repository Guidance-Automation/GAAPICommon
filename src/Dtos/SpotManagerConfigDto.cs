using ProtoBuf;
using System;

namespace GAAPICommon.Core.Dtos;

[ProtoContract]
public class SpotManagerConfigDto
{
    [ProtoMember( 1)]
    public TimeSpan IdleTimeout { get; set; } = TimeSpan.MinValue;

    [ProtoMember( 2)]
    public double ChargeDowngradeLevel { get; set; } = double.NaN;

    [ProtoMember( 3)]
    public double ImmediateChargeLevel { get; set; } = double.NaN;
}