namespace GAAPICommon.Messages;

public partial class FleetManagementMetadataDto
{
    public FleetManagementMetadataDto(string? productName, SemVerDto? version)
    {
        if (string.IsNullOrEmpty(productName))
            throw new ArgumentOutOfRangeException(nameof(productName));

        ProductName = productName;

        SemVer = version ?? throw new ArgumentNullException(nameof(version));
    }
}