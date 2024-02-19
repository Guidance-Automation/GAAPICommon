namespace GAAPICommon.Messages;

public partial class FleetManagementMetadata
{
    public FleetManagementMetadata(string? productName, SemVer? version)
    {
        if (string.IsNullOrEmpty(productName))
            throw new ArgumentOutOfRangeException(nameof(productName));

        ProductName = productName;

        SemVer = version ?? throw new ArgumentNullException(nameof(version));
    }
}