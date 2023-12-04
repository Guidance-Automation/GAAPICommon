using NUnit.Framework;
using Moq;

namespace GAAPICommon.Architecture.Test;

[TestFixture]
public class TExtensionMethods
{
    [TestCase(PositionControlStatus.OK, false)]
    [TestCase(PositionControlStatus.NoWaypoints, false)]
    [TestCase(PositionControlStatus.OutOfPosition, true)]
    [TestCase(PositionControlStatus.WaypointDiscontinuity, true)]
    public void PCSFaultState(PositionControlStatus pcs, bool isFault)
    {
        Assert.That(isFault, Is.EqualTo(pcs.IsFault()));
    }


    [TestCase(false)]
    [TestCase(true, PositionControlStatus.OutOfPosition)]
    public void IsInFault(bool expected, 
        PositionControlStatus pcs = PositionControlStatus.OK,
        NavigationStatus ns = NavigationStatus.OK,
        DynamicLimiterStatus dls = DynamicLimiterStatus.OK,
        ExtendedDataFaultStatus edfs = ExtendedDataFaultStatus.OK)
    {
        Mock<IKingpinState> mock = new();

        mock.Setup(e => e.PositionControlStatus).Returns(pcs);
        mock.Setup(e => e.NavigationStatus).Returns(ns);
        mock.Setup(e => e.DynamicLimiterStatus).Returns(dls);
        mock.Setup(e => e.ExtendedDataFaultStatus).Returns(edfs);

        IKingpinState kingpinState = mock.Object;

        Assert.That(expected, Is.EqualTo(kingpinState.IsInFault()));
    }
}