using Crud.DDD.Core.Common.Exeptions;
using Crud.DDD.Core.Common.ValueObjects;

namespace Crud.DDD.Domain.Test.User
{

    public class EmailTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Email_Should_ThrowBusinessException(string? address)
        {
            Email Action() => new Email(address);

            Assert.Throws<BusinessException>(() => Action());
        }

        [Theory]
        [InlineData("Pouya@gmail.com")]
        public void Email_ShouldReturnValidEmail(string address)
        {
            Email Action() => new Email(address);

            Assert.Equal((Action().Address), address);
        }

        [Theory]
        [InlineData("Pouyagmail.com")]
        public void Email_Should_Throw_BusinessException_WhenNotMatchWithPattern(string address)
        {
            Email Action() => Email.Create(address);

            Assert.Throws<BusinessException>(Action);
        }

        [Theory]
        [InlineData("Pouya@gmail.com")]
        public void Email_Should_ReturnValidEmail_WhenAddressIsMatchWithPattern(string address)
        {
            Email Action() => Email.Create(address);

            Assert.Equal((Action()).Address, address);
        }
    }
}
