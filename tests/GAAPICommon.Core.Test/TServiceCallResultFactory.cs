using GAAPICommon.Architecture;
using GAAPICommon.Core.Dtos;
using NUnit.Framework;
using System;

namespace GAAPICommon.Core.Test
{
    [TestFixture]
    public class TServiceCallResultFactory
    {
        [TestCase(VideoGraves.Wonderswan)]
        public void FromGenericEnum(VideoGraves videoGraves)
        {
            ServiceCallResultDto result = ServiceCallResultFactory.FromError(videoGraves);

            Assert.IsNotNull(result);
            Assert.AreEqual((int)videoGraves, result.ServiceCode);
        }

        [TestCase(VideoGraves.Wonderswan)]
        public void FromGenericEnumWithValue(VideoGraves videoGraves)
        {
            ServiceCallResultFactory<string>.FromError(videoGraves);
            ServiceCallResultDto<string> result = ServiceCallResultFactory<string>.FromError(videoGraves);

            Assert.IsNotNull(result);
            Assert.AreEqual((int)videoGraves, result.ServiceCode);
            Assert.AreEqual(default(string), result.Value);
        }


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