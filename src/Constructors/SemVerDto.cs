using GAAPICommon.Enums;
using System.Globalization;
using System.Text.RegularExpressions;

namespace GAAPICommon.Messages;

/// <summary>
/// Represents a Semantic Versioning data transfer object that encapsulates versioning information
/// with major, minor, patch numbers, and a release flag.
/// </summary>
public partial class SemVerDto
{
    private static readonly Dictionary<ReleaseFlag, string> _releaseFlagDictionary = new()
    {
        {Enums.ReleaseFlag.Alpha, "alpha" },
        {Enums.ReleaseFlag.Beta, "beta" },
        {Enums.ReleaseFlag.ReleaseCandidate, "rc" },
        {Enums.ReleaseFlag.Release, "" }
    };

    /// <summary>
    /// Initializes a new instance of <see cref="SemVerDto"/> class using a <see cref="Version"/> object. Defaults to <see cref="ReleaseFlag.Release"/>
    /// </summary>
    /// <param name="version">The version object containing major, minor, build, and revision components.</param>
    public SemVerDto(Version version)
    {
        Major = version.Major;
        Minor = version.Minor;
        Patch = version.Build;
        ReleaseFlag = _releaseFlagDictionary[Enums.ReleaseFlag.Release];
    }

    /// <summary>
    /// Initializes a new instance of <see cref="SemVerDto"/> class using a <see cref="Version"/> object and a <see cref="Enums.ReleaseFlag"/>
    /// </summary>
    /// <param name="version">The version object containing major, minor, build, and revision components.</param>
    /// <param name="releaseFlag">The release flag enum to define the state of release.</param>
    public SemVerDto(Version version, ReleaseFlag releaseFlag)
    {
        Major = version.Major;
        Minor = version.Minor;
        Patch = version.Build;
        ReleaseFlag = _releaseFlagDictionary[releaseFlag];
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

    /// <summary>
    /// Convert this object into a formatted string defining the version.
    /// </summary>
    /// <returns>A string in the format "Major.Minor.Patch-ReleaseFlag"</returns>
    public string ToVersionString()
    {
        if (string.IsNullOrEmpty(ReleaseFlag))
            return $"{Major}.{Minor}.{Patch}";
        else
            return $"{Major}.{Minor}.{Patch}-{ReleaseFlag}";
    }

    private static readonly Dictionary<string, ReleaseFlag> _stringToReleaseFlag =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "alpha", Enums.ReleaseFlag.Alpha },
            { "beta",  Enums.ReleaseFlag.Beta },
            { "rc",    Enums.ReleaseFlag.ReleaseCandidate },
            { "",      Enums.ReleaseFlag.Release }
    };

    private static readonly Regex _semVerRegex = VersionRegex();

    /// <summary>
    /// Parses a semantic version string into a <see cref="SemVerDto"/>.
    /// Supported formats:
    /// - MAJOR.MINOR.PATCH
    /// - MAJOR.MINOR.PATCH-flag (alpha, beta, rc)
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="FormatException"/>
    public static SemVerDto FromString(string value)
    {
        value = value.Trim();

        Match match = _semVerRegex.Match(value);
        if (!match.Success)
            throw new FormatException($"Invalid semantic version format: '{value}'");

        int major = int.Parse(match.Groups["major"].Value, CultureInfo.InvariantCulture);
        int minor = int.Parse(match.Groups["minor"].Value, CultureInfo.InvariantCulture);
        int patch = int.Parse(match.Groups["patch"].Value, CultureInfo.InvariantCulture);

        string flagString = match.Groups["flag"].Success
            ? match.Groups["flag"].Value
            : string.Empty;

        if (!_stringToReleaseFlag.TryGetValue(flagString, out ReleaseFlag releaseFlag))
            throw new FormatException($"Unknown release flag '{flagString}' in '{value}'");

        return new SemVerDto(major, minor, patch, releaseFlag);
    }

    [GeneratedRegex(@"^(?<major>\d+)\.(?<minor>\d+)\.(?<patch>\d+)(?:-(?<flag>[A-Za-z]+))?$", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex VersionRegex();
}