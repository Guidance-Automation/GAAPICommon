using GAAPICommon.Enums;
using GAAPICommon.Messages;
using Google.Protobuf;
using NUnit.Framework;
using System.Reflection;

namespace GAAPICommon.Tests;

[TestFixture]
public class SemVerDtoTests
{
    [Test]
    public void FromString_ParsesReleaseCandidateDisplayVersion()
    {
        SemVerDto version = SemVerDto.FromString("1.0.0-rc.1");

        Assert.Multiple(() =>
        {
            Assert.That(version.Major, Is.EqualTo(1));
            Assert.That(version.Minor, Is.Zero);
            Assert.That(version.Patch, Is.Zero);
            Assert.That(version.ReleaseFlag, Is.EqualTo(ReleaseFlag.ReleaseCandidate));
            Assert.That(version.PreRelease, Is.EqualTo("1"));
            Assert.That(version.BuildMetadata, Is.Empty);
            Assert.That(version.ToSemanticVersionString(), Is.EqualTo("1.0.0-rc.1"));
            Assert.That(version.ToVersionString(), Is.EqualTo("1.0.0-rc.1"));
        });
    }

    [Test]
    public void FromString_ParsesReleaseDisplayVersion()
    {
        SemVerDto version = SemVerDto.FromString("1.0.0");

        Assert.Multiple(() =>
        {
            Assert.That(version.ReleaseFlag, Is.EqualTo(ReleaseFlag.Release));
            Assert.That(version.PreRelease, Is.Empty);
            Assert.That(version.ToSemanticVersionString(), Is.EqualTo("1.0.0"));
            Assert.That(version.ToVersionString(), Is.EqualTo("1.0.0"));
        });
    }

    [Test]
    public void FromString_PreservesBuildMetadata()
    {
        SemVerDto version = SemVerDto.FromString("1.2.3-beta.2+build.7");

        Assert.Multiple(() =>
        {
            Assert.That(version.ReleaseFlag, Is.EqualTo(ReleaseFlag.Beta));
            Assert.That(version.PreRelease, Is.EqualTo("2"));
            Assert.That(version.BuildMetadata, Is.EqualTo("build.7"));
            Assert.That(version.ToSemanticVersionString(), Is.EqualTo("1.2.3-beta.2+build.7"));
        });
    }

    [TestCase("01.0.0")]
    [TestCase("1.01.0")]
    [TestCase("1.0.01")]
    [TestCase("1.0.0-preview.1")]
    [TestCase("1.0.0-rc.01")]
    [TestCase("1.0.0-rc.")]
    [TestCase("v1.0.0")]
    [TestCase("2147483648.0.0")]
    public void FromString_RejectsInvalidOrUnsupportedVersions(string value)
    {
        Assert.That(
            () => SemVerDto.FromString(value),
            Throws.TypeOf<FormatException>());
    }

    [Test]
    public void CompareTo_UsesSemanticVersionPrecedence()
    {
        SemVerDto[] ordered =
        [
            SemVerDto.FromString("1.0.0-alpha"),
            SemVerDto.FromString("1.0.0-alpha.1"),
            SemVerDto.FromString("1.0.0-beta"),
            SemVerDto.FromString("1.0.0-rc.1"),
            SemVerDto.FromString("1.0.0")
        ];

        for (int index = 0; index < ordered.Length - 1; index++)
            Assert.That(ordered[index].CompareTo(ordered[index + 1]), Is.LessThan(0));
    }

    [Test]
    public void CompareTo_IgnoresBuildMetadata()
    {
        SemVerDto left = SemVerDto.FromString("1.0.0+build.1");
        SemVerDto right = SemVerDto.FromString("1.0.0+build.99");

        Assert.That(left.CompareTo(right), Is.Zero);
    }

    [Test]
    public void CompareTo_OrdersNumericIdentifiersLargerThanInt32()
    {
        SemVerDto left = SemVerDto.FromString("1.0.0-rc.99999999999999999999");
        SemVerDto right = SemVerDto.FromString("1.0.0-rc.100000000000000000000");

        Assert.That(left.CompareTo(right), Is.LessThan(0));
    }

    [Test]
    public void ProtobufRoundTrip_PreservesSemanticVersion()
    {
        SemVerDto expected = SemVerDto.FromString("1.0.0-rc.1+sha.123");

        SemVerDto actual = SemVerDto.Parser.ParseFrom(expected.ToByteArray());

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void FromAssembly_UsesInformationalVersion()
    {
        Assembly assembly = typeof(SemVerDtoTests).Assembly;

        SemVerDto version = SemVerDto.FromAssembly(assembly);

        Assert.That(version.ToVersionString(), Is.EqualTo("1.0.0-rc.1"));
    }

    [Test]
    public void Constructor_RejectsPrereleaseIdentifiersForRelease()
    {
        Assert.That(
            () => new SemVerDto(1, 0, 0, ReleaseFlag.Release, "1"),
            Throws.ArgumentException);
    }
}
