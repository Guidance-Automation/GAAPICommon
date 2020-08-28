using NUnit.Framework;

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
    }
}