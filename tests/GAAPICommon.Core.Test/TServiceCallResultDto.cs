using GAAPICommon.Architecture;
using GAAPICommon.Core.Dtos;
using NUnit.Framework;

namespace GAAPICommon.Core.Test;

[TestFixture]
public class TServiceCallResultDto
{
    [Test]
    public void ArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            Exception ex = new("Ohes noes!");

            ServiceCallResultDto dto = new(0, ex);
        });
    }

    [Test]
    public void FromClientException()
    {
        Exception ex = new("Ohes noes");

        ServiceCallResultDto dto = ServiceCallResultFactory.FromClientException(ex);

        Assert.Multiple(() =>
        {
            Assert.That(dto.ServiceCode, Is.EqualTo((int)ServiceCode.ClientException));
            Assert.That(string.Equals(ex.Message, dto.ExceptionMessage, StringComparison.OrdinalIgnoreCase));
        });
    }
}