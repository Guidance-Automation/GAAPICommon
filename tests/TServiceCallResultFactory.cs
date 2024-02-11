using GAAPICommon.Architecture;
using GAAPICommon.Core.Dtos;
using NUnit.Framework;

namespace GAAPICommon.Core.Test;

[TestFixture]
public class TServiceCallResultFactory
{
    [Test]
    public void FromCaughtException()
    {
        int exceptionId = 66;
        Exception ex = new("Ohes noes");

        ServiceCallResultDto dto = ServiceCallResultFactory.FromCaughtException(exceptionId, ex);

        Assert.Multiple(() =>
        {
            Assert.That(dto.IsSuccessful(), Is.False);
            Assert.That(exceptionId, Is.EqualTo(dto.ServiceCode));
            Assert.That(string.Equals(ex.Message, dto.ExceptionMessage, StringComparison.OrdinalIgnoreCase));
            Assert.That(string.Equals(ex.Source, dto.ExceptionSource, StringComparison.OrdinalIgnoreCase));
            Assert.That(string.Equals(ex.StackTrace, dto.ExceptionStackTrace, StringComparison.OrdinalIgnoreCase));
        });
    }

    [Test]
    public void FromNoError()
    {
        ServiceCallResultDto dto = ServiceCallResultFactory.FromNoError();

        Assert.Multiple(() =>
        {
            Assert.That(dto.IsSuccessful());
            Assert.That(dto.ServiceCode, Is.EqualTo(0));
            Assert.That(dto.ExceptionMessage, Is.Null);
            Assert.That(dto.ExceptionSource, Is.Null);
            Assert.That(dto.ExceptionStackTrace, Is.Null);
        });
    }
}