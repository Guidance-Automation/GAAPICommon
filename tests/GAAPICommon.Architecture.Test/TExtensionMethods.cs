using NUnit.Framework;
using Moq;

namespace GAAPICommon.Architecture.Test
{
    [TestFixture]
    public class TExtensionMethods
    {
        [TestCase(PositionControlStatus.OK, false)]
        [TestCase(PositionControlStatus.NoWaypoints, false)]
        [TestCase(PositionControlStatus.OutOfPosition, true)]
        [TestCase(PositionControlStatus.WaypointDiscontinuity, true)]
        public void PCSFaultState(PositionControlStatus pcs, bool isFault)
        {
            Assert.AreEqual(isFault, pcs.IsFault());
        }


        [TestCase(false)]
        [TestCase(true, PositionControlStatus.OutOfPosition)]
        public void IsInFault(bool expected, 
            PositionControlStatus pcs = PositionControlStatus.OK,
            NavigationStatus ns = NavigationStatus.OK,
            DynamicLimiterStatus dls = DynamicLimiterStatus.OK,
            ExtendedDataFaultStatus edfs = ExtendedDataFaultStatus.OK)
        {
            Mock<IKingpinState> mock = new Mock<IKingpinState>();

            mock.Setup(e => e.PositionControlStatus).Returns(pcs);
            mock.Setup(e => e.NavigationStatus).Returns(ns);
            mock.Setup(e => e.DynamicLimiterStatus).Returns(dls);
            mock.Setup(e => e.ExtendedDataFaultStatus).Returns(edfs);

            IKingpinState kingpinState = mock.Object;

            Assert.AreEqual(expected, kingpinState.IsInFault());
        }
    }
}