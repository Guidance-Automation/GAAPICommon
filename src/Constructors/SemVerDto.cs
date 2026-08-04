using GAAPICommon.Enums;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace GAAPICommon.Messages;

/// <summary>
/// Represents a Semantic Versioning 2.0 version using the release stages supported by moNitrav.
/// </summary>
/// <remarks>
/// The wire contract stores the prerelease stage separately from its remaining dot-separated
/// identifiers. For example, <c>1.0.0-rc.1</c> is represented by
/// <see cref="ReleaseFlag.ReleaseCandidate"/> and <c>1</c>.
/// </remarks>
public partial class SemVerDto : IComparable, IComparable<SemVerDto>
{
    private static readonly Dictionary<ReleaseFlag, string> _releaseFlagDictionary = new()
    {
        { ReleaseFlag.Alpha, "alpha" },
        { ReleaseFlag.Beta, "beta" },
        { ReleaseFlag.ReleaseCandidate, "rc" },
        { ReleaseFlag.Release, string.Empty }
    };

    private static readonly Dictionary<string, ReleaseFlag> _stringToReleaseFlag =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "alpha", ReleaseFlag.Alpha },
            { "beta", ReleaseFlag.Beta },
            { "rc", ReleaseFlag.ReleaseCandidate },
            { string.Empty, ReleaseFlag.Release }
        };

    private static readonly Regex _semVerRegex = VersionRegex();

    /// <summary>
    /// Initializes a release version from the first three components of a <see cref="Version"/>.
    /// </summary>
    public SemVerDto(Version version)
        : this(
            version?.Major ?? throw new ArgumentNullException(nameof(version)),
            version.Minor,
            GetPatch(version),
            ReleaseFlag.Release)
    {
    }

    /// <summary>
    /// Initializes a version from a <see cref="Version"/>, release stage, and optional prerelease
    /// and build metadata identifiers.
    /// </summary>
    public SemVerDto(
        Version version,
        ReleaseFlag releaseFlag,
        string preRelease = "",
        string buildMetadata = "")
        : this(
            version?.Major ?? throw new ArgumentNullException(nameof(version)),
            version.Minor,
            GetPatch(version),
            releaseFlag,
            preRelease,
            buildMetadata)
    {
    }

    /// <summary>
    /// Initializes a semantic version.
    /// </summary>
    public SemVerDto(
        int major,
        int minor,
        int patch,
        ReleaseFlag releaseFlag = ReleaseFlag.Release,
        string preRelease = "",
        string buildMetadata = "")
    {
        if (major < 0)
            throw new ArgumentOutOfRangeException(nameof(major), "Major version cannot be negative.");

        if (minor < 0)
            throw new ArgumentOutOfRangeException(nameof(minor), "Minor version cannot be negative.");

        if (patch < 0)
            throw new ArgumentOutOfRangeException(nameof(patch), "Patch version cannot be negative.");

        if (!_releaseFlagDictionary.ContainsKey(releaseFlag))
            throw new ArgumentOutOfRangeException(nameof(releaseFlag), "A release stage must be specified.");

        preRelease ??= string.Empty;
        buildMetadata ??= string.Empty;

        if (releaseFlag == ReleaseFlag.Release && !string.IsNullOrEmpty(preRelease))
        {
            throw new ArgumentException(
                "A release version cannot have prerelease identifiers.",
                nameof(preRelease));
        }

        if (!string.IsNullOrEmpty(preRelease) && !IsValidPreReleaseIdentifiers(preRelease))
        {
            throw new ArgumentException(
                $"Invalid prerelease identifiers '{preRelease}'.",
                nameof(preRelease));
        }

        if (!string.IsNullOrEmpty(buildMetadata) && !IdentifierListRegex().IsMatch(buildMetadata))
        {
            throw new ArgumentException(
                $"Invalid build metadata '{buildMetadata}'.",
                nameof(buildMetadata));
        }

        Major = major;
        Minor = minor;
        Patch = patch;
        ReleaseFlag = releaseFlag;
        PreRelease = preRelease;
        BuildMetadata = buildMetadata;
    }

    /// <summary>
    /// Parses a semantic version.
    /// </summary>
    public static SemVerDto FromString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));

        value = value.Trim();

        Match match = _semVerRegex.Match(value);
        if (!match.Success)
            throw new FormatException($"Invalid semantic version format: '{value}'");

        if (!int.TryParse(
                match.Groups["major"].Value,
                NumberStyles.None,
                CultureInfo.InvariantCulture,
                out int major)
            || !int.TryParse(
                match.Groups["minor"].Value,
                NumberStyles.None,
                CultureInfo.InvariantCulture,
                out int minor)
            || !int.TryParse(
                match.Groups["patch"].Value,
                NumberStyles.None,
                CultureInfo.InvariantCulture,
                out int patch))
        {
            throw new FormatException(
                $"Semantic version components must fit in a 32-bit integer: '{value}'");
        }

        string flagString = match.Groups["flag"].Success
            ? match.Groups["flag"].Value
            : string.Empty;

        if (!_stringToReleaseFlag.TryGetValue(flagString, out ReleaseFlag releaseFlag))
            throw new FormatException($"Unknown release flag '{flagString}' in '{value}'");

        string preRelease = match.Groups["pre"].Success
            ? match.Groups["pre"].Value
            : string.Empty;

        string buildMetadata = match.Groups["build"].Success
            ? match.Groups["build"].Value
            : string.Empty;

        try
        {
            return new SemVerDto(
                major,
                minor,
                patch,
                releaseFlag,
                preRelease,
                buildMetadata);
        }
        catch (ArgumentException ex)
        {
            throw new FormatException($"Invalid semantic version format: '{value}'", ex);
        }
    }

    /// <summary>
    /// Reads a semantic version from an assembly's informational version, falling back to its
    /// numeric assembly version when informational metadata is unavailable.
    /// </summary>
    public static SemVerDto FromAssembly(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        string? informationalVersion = assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion;

        if (!string.IsNullOrWhiteSpace(informationalVersion))
            return FromString(informationalVersion);

        Version version = assembly.GetName().Version
            ?? throw new InvalidOperationException($"Assembly '{assembly.FullName}' has no version.");

        return new SemVerDto(version);
    }

    /// <summary>
    /// Returns the canonical Semantic Versioning value without a display prefix.
    /// </summary>
    public string ToSemanticVersionString()
    {
        string releaseFlag = GetReleaseFlagText(ReleaseFlag);
        string result = $"{Major}.{Minor}.{Patch}";

        if (!string.IsNullOrEmpty(releaseFlag))
        {
            result += $"-{releaseFlag}";

            if (!string.IsNullOrEmpty(PreRelease))
                result += $".{PreRelease}";
        }

        if (!string.IsNullOrEmpty(BuildMetadata))
            result += $"+{BuildMetadata}";

        return result;
    }

    /// <summary>
    /// Returns the semantic version, such as <c>1.0.0-rc.1</c> or <c>1.0.0</c>.
    /// </summary>
    public string ToVersionString()
    {
        return ToSemanticVersionString();
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is not SemVerDto other)
            throw new ArgumentException("Object must be a SemVerDto.", nameof(obj));

        return CompareTo(other);
    }

    /// <inheritdoc />
    public int CompareTo(SemVerDto? other)
    {
        if (other is null)
            return 1;

        int result = Major.CompareTo(other.Major);
        if (result != 0) return result;

        result = Minor.CompareTo(other.Minor);
        if (result != 0) return result;

        result = Patch.CompareTo(other.Patch);
        if (result != 0) return result;

        bool thisIsRelease = ReleaseFlag == ReleaseFlag.Release;
        bool otherIsRelease = other.ReleaseFlag == ReleaseFlag.Release;

        if (thisIsRelease && otherIsRelease) return 0;
        if (thisIsRelease) return 1;
        if (otherIsRelease) return -1;

        result = GetReleaseRank(ReleaseFlag).CompareTo(GetReleaseRank(other.ReleaseFlag));
        if (result != 0) return result;

        return ComparePreRelease(PreRelease, other.PreRelease);
    }

    private static string GetReleaseFlagText(ReleaseFlag releaseFlag)
    {
        if (!_releaseFlagDictionary.TryGetValue(releaseFlag, out string? value))
            throw new InvalidOperationException("The semantic version has no release stage.");

        return value;
    }

    private static int GetReleaseRank(ReleaseFlag flag) => flag switch
    {
        ReleaseFlag.Alpha => 0,
        ReleaseFlag.Beta => 1,
        ReleaseFlag.ReleaseCandidate => 2,
        ReleaseFlag.Release => 3,
        _ => throw new InvalidOperationException($"Unknown release flag '{flag}'.")
    };

    private static int ComparePreRelease(string? left, string? right)
    {
        if (string.IsNullOrEmpty(left) && string.IsNullOrEmpty(right)) return 0;
        if (string.IsNullOrEmpty(left)) return -1;
        if (string.IsNullOrEmpty(right)) return 1;

        string[] leftParts = left.Split('.');
        string[] rightParts = right.Split('.');
        int max = Math.Max(leftParts.Length, rightParts.Length);

        for (int i = 0; i < max; i++)
        {
            if (i >= leftParts.Length) return -1;
            if (i >= rightParts.Length) return 1;

            string leftPart = leftParts[i];
            string rightPart = rightParts[i];

            bool leftIsNumber = leftPart.All(char.IsAsciiDigit);
            bool rightIsNumber = rightPart.All(char.IsAsciiDigit);

            if (leftIsNumber && rightIsNumber)
            {
                int numericResult = leftPart.Length.CompareTo(rightPart.Length);
                if (numericResult == 0)
                    numericResult = string.CompareOrdinal(leftPart, rightPart);

                if (numericResult != 0) return numericResult;
            }
            else if (leftIsNumber)
            {
                return -1;
            }
            else if (rightIsNumber)
            {
                return 1;
            }
            else
            {
                int textResult = string.CompareOrdinal(leftPart, rightPart);
                if (textResult != 0) return textResult;
            }
        }

        return 0;
    }

    private static bool IsValidPreReleaseIdentifiers(string value)
    {
        if (!IdentifierListRegex().IsMatch(value))
            return false;

        foreach (string identifier in value.Split('.'))
        {
            if (identifier.Length > 1
                && identifier[0] == '0'
                && identifier.All(char.IsAsciiDigit))
            {
                return false;
            }
        }

        return true;
    }

    private static int GetPatch(Version version)
    {
        return version.Build < 0 ? 0 : version.Build;
    }

    [GeneratedRegex(
        @"^(?<major>0|[1-9]\d*)\.(?<minor>0|[1-9]\d*)\.(?<patch>0|[1-9]\d*)(?:-(?<flag>alpha|beta|rc)(?:\.(?<pre>[0-9A-Za-z-]+(?:\.[0-9A-Za-z-]+)*))?)?(?:\+(?<build>[0-9A-Za-z-]+(?:\.[0-9A-Za-z-]+)*))?$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase)]
    private static partial Regex VersionRegex();

    [GeneratedRegex(
        @"^[0-9A-Za-z-]+(?:\.[0-9A-Za-z-]+)*$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex IdentifierListRegex();
}
