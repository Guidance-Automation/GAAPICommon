using GAAPICommon.Enums;

namespace GAAPICommon.Messages;
public partial class SemVer
{
    private static readonly Dictionary<ReleaseFlag, string> _releaseFlagDictionary = new()
    {
        {Enums.ReleaseFlag.Alpha, "Alpha" },
        {Enums.ReleaseFlag.Beta, "Beta" },
        {Enums.ReleaseFlag.ReleaseCandidate, "Release candidate" },
        {Enums.ReleaseFlag.Release, "Release" }
    };

    public SemVer(Version version)
    {
        ArgumentNullException.ThrowIfNull(version);

        Major = version.Major;
        Minor = version.Minor;
        Patch = version.Build;
        ReleaseFlag = _releaseFlagDictionary[(ReleaseFlag)version.Revision];
    }

    public SemVer(int major, int minor, int patch, ReleaseFlag releaseFlag)
    {
        Major = major;
        Minor = minor;
        Patch = patch;
        ReleaseFlag = _releaseFlagDictionary[releaseFlag];
    }

    public int CompareTo(object? obj)
    {
        if (obj == null) return 1;

        if (obj is not SemVer other) throw new InvalidOperationException("Object is not SemVer");

        int majorResult = Major.CompareTo(other.Major);

        if (majorResult != 0) return majorResult;

        int minorResult = Minor.CompareTo(other.Minor);

        if (minorResult != 0) return minorResult;

        int patchResult = Patch.CompareTo(other.Patch);

        if (patchResult != 0) return patchResult;

        return ReleaseFlag.CompareTo(other.ReleaseFlag);
    }
}