using GAAPICommon.Enums;

namespace GAAPICommon.Messages;

/// <summary>
/// Represents a Semantic Versioning data transfer object that encapsulates versioning information
/// with major, minor, patch numbers, and a release flag.
/// </summary>
public partial class SemVerDto
{
    private static readonly Dictionary<ReleaseFlag, string> _releaseFlagDictionary = new()
    {
        {Enums.ReleaseFlag.Alpha, "Alpha" },
        {Enums.ReleaseFlag.Beta, "Beta" },
        {Enums.ReleaseFlag.ReleaseCandidate, "ReleaseCandidate" },
        {Enums.ReleaseFlag.Release, "Release" }
    };

    /// <summary>
    /// Initializes a new instance of <see cref="SemVerDto"/> class using a <see cref="Version"/> object.
    /// </summary>
    /// <param name="version">The version object containing major, minor, build, and revision components.</param>
    public SemVerDto(Version version)
    {
        ArgumentNullException.ThrowIfNull(version);

        Major = version.Major;
        Minor = version.Minor;
        Patch = version.Build;
        ReleaseFlag = _releaseFlagDictionary[(ReleaseFlag)version.Revision];
    }

    /// <summary>
    /// Initializes a new instance of <see cref="SemVerDto"/> class with explicit major, minor, patch, and release flag values.
    /// </summary>
    /// <param name="major">Major version number.</param>
    /// <param name="minor">Minor version number.</param>
    /// <param name="patch">Patch number.</param>
    /// <param name="releaseFlag">Release flag as defined in <see cref="ReleaseFlag"/>.</param>
    public SemVerDto(int major, int minor, int patch, ReleaseFlag releaseFlag)
    {
        Major = major;
        Minor = minor;
        Patch = patch;
        ReleaseFlag = _releaseFlagDictionary[releaseFlag];
    }

    /// <summary>
    /// Compares the current instance with another object of the same type and returns an integer that indicates
    /// whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the object is not a <see cref="SemVerDto"/>.</exception>
    public int CompareTo(object? obj)
    {
        if (obj == null) return 1;

        if (obj is not SemVerDto other) throw new InvalidOperationException("Object is not SemVer");

        int majorResult = Major.CompareTo(other.Major);

        if (majorResult != 0) return majorResult;

        int minorResult = Minor.CompareTo(other.Minor);

        if (minorResult != 0) return minorResult;

        int patchResult = Patch.CompareTo(other.Patch);

        if (patchResult != 0) return patchResult;

        return ReleaseFlag.CompareTo(other.ReleaseFlag);
    }
}