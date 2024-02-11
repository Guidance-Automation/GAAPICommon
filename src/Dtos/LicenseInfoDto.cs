using ProtoBuf;
using System;

namespace GAAPICommon.Core.Dtos;

[ProtoContract]
public class LicenseInfoDto
{
    [ProtoMember(1)]
    public string CustomerName { get; set; }

    [ProtoMember(2)]
    public int CustomerId { get; set; }

    [ProtoMember(3)]
    public string Manufacturer { get; set; }

    [ProtoMember(4)]
    public int Uid { get; set; }

    [ProtoMember(5)]
    public DateTime CreateDate { get; set; }

    [ProtoMember(6)]
    public DateTime LastUpdate { get; set; }

    [ProtoMember(7)]
    public string LicenseKey { get; set; }

    [ProtoMember(8)]
    public string Vendor { get; set; }

    [ProtoMember(9)]
    public int FleetLimit { get; set; }

    [ProtoMember(10)]
    public int RentalFleetLimit { get; set; }

    [ProtoMember(11)]
    public int LicensedVersion { get; set; }

    [ProtoMember(12)]
    public string LicensedProduct { get; set; }

    [ProtoMember(13)]
    public bool MaintenanceStatus { get; set; }

    [ProtoMember(14)]
    public DateTime ExpiryDate { get; set; }

    [ProtoMember(15)]
    public DateTime PurchasedFleetExpiryDate { get; set; }

    [ProtoMember(16)]
    public DateTime RentalExpiryDate { get; set; }

    [ProtoMember(17)]
    public DateTime FleetMaintenanceExpiry { get; set; }

    [ProtoMember(18)]
    public DateTime RentalFleetMaintenanceExpiry { get; set; }
}