using System.Runtime.Serialization;

namespace GAAPICommon.Core.Dtos;

[DataContract]
public class LicenseInfoDto
{
    [DataMember]
    public string? CustomerName { get; set; }

    [DataMember]
    public int CustomerId { get; set; }

    [DataMember]
    public string? Manufacturer { get; set; }

    [DataMember]
    public int Uid { get; set; }

    [DataMember]
    public DateTime CreateDate { get; set; }

    [DataMember]
    public DateTime LastUpdate { get; set; }

    [DataMember]
    public string? LicenseKey { get; set; }

    [DataMember]
    public string? Vendor { get; set; }

    [DataMember]
    public int FleetLimit { get; set; }

    [DataMember]
    public int RentalFleetLimit { get; set; }

    [DataMember]
    public int LicensedVersion { get; set; }

    [DataMember]
    public string? LicensedProduct { get; set; }

    [DataMember]
    public bool MaintenanceStatus { get; set; }

    [DataMember]
    public DateTime ExpiryDate { get; set; }

    [DataMember]
    public DateTime PurchasedFleetExpiryDate { get; set; }

    [DataMember]
    public DateTime RentalExpiryDate { get; set; }

    [DataMember]
    public DateTime FleetMaintenanceExpiry { get; set; }

    [DataMember]
    public DateTime RentalFleetMaintenanceExpiry { get; set; }
}