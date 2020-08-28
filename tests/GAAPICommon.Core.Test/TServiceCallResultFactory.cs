using GAAPICommon.Architecture;
using GAAPICommon.Core.Dtos;
using NUnit.Framework;
using System;

namespace GAAPICommon.Core.Test
{
    [TestFixture]
    public class TServiceCallResultFactory
    {
        [Test]
        public void FromCaughtException()
        {
            int exceptionId = 66;
            Exception ex = new Exception("Ohes noes");

            ServiceCallResultDto dto = ServiceCallResultFactory.FromCaughtException(exceptionId, ex);

            Assert.IsFalse(dto.IsSuccessful());

            Assert.AreEqual(exceptionId, dto.ServiceCode);
            StringAssert.AreEqualIgnoringCase(ex.Message, dto.ExceptionMessage);
            StringAssert.AreEqualIgnoringCase(ex.Source, dto.ExceptionSource);
            StringAssert.AreEqualIgnoringCase(ex.StackTrace, dto.ExceptionStackTrace);
        }

        [Test]
        public void FromNoError()
        {
            ServiceCallResultDto dto = ServiceCallResultFactory.FromNoError();

            Assert.IsTrue(dto.IsSuccessful());

            Assert.AreEqual(0, dto.ServiceCode);
            Assert.IsNull(dto.ExceptionMessage);
            Assert.IsNull(dto.ExceptionSource);
            Assert.IsNull(dto.ExceptionStackTrace);
        }
    }
}