namespace GAAPICommon.Messages;

/// <summary>
/// Represents metadata associated with fleet management, containing information such as the product name
/// and its version.
/// </summary>
public partial class FleetManagementMetadataDto
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FleetManagementMetadataDto"/> class.
    /// </summary>
    /// <param name="productName">The name of the product. Must not be null or empty.</param>
    /// <param name="version">The semantic versioning information of the product as a <see cref="SemVerDto"/> object.
    /// Must not be null.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="productName"/> is null or empty.</exception>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="version"/> is null.</exception>
    public FleetManagementMetadataDto(string? productName, SemVerDto? version)
    {
        if (string.IsNullOrEmpty(productName))
            throw new ArgumentOutOfRangeException(nameof(productName));

        ProductName = productName;

        SemVer = version ?? throw new ArgumentNullException(nameof(version));
    }
}