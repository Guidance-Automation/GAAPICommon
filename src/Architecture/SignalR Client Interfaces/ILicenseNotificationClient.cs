using System.Threading.Tasks;

namespace GAAPICommon.Architecture;

public interface ILicenseNotificationClient
{
    /// <summary>
    /// Will fire any time the license becomes invalid
    /// </summary>
    /// <param name="failureReason">The reason the license was declared invalid</param>
    /// <returns></returns>
    public Task LicenseInvalid(string failureReason);

    /// <summary>
    /// Will fire when an element of the license changes
    /// </summary>
    /// <param name="changedElement">The element of the license that has changed</param>
    /// <returns></returns>
    public Task LicenseChanged(string changedElement);

    /// <summary>
    /// Will fire once a day when one or more of the expiry dates is within 30 days of expiry
    /// </summary>
    /// <param name="daysRemaining">The days remaining until the reported expiry will expire</param>
    /// <param name="expiryType">The expiry type that is being warned of: Software, Fleet and Rental Fleet</param>
    /// <returns></returns>
    public Task ExpiryWarning(int daysRemaining, string expiryType);
}