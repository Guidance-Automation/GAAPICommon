namespace GAAPICommon.Interfaces;

/// <summary>
/// Defines the interface for a Licence notification client.
/// </summary>
public interface ILicenceNotificationClient
{
    /// <summary>
    /// Will fire any time the licence becomes invalid.
    /// </summary>
    /// <param name="failureReason">The reason the licence was declared invalid.</param>
    public Task LicenceInvalid(string failureReason);

    /// <summary>
    /// Will fire when an element of the licence changes.
    /// </summary>
    /// <param name="changedElement">The element of the licence that has changed.</param>
    public Task LicenceChanged(string changedElement);

    /// <summary>
    /// Will fire once a day when one or more of the expiry dates is within 30 days of expiry
    /// </summary>
    /// <param name="daysRemaining">The days remaining until the reported expiry will expire</param>
    /// <param name="expiryType">The expiry type that is being warned of: Software, Fleet and Rental Fleet</param>
    public Task ExpiryWarning(int daysRemaining, string expiryType);
}