using Crud.DDD.Core.Common.ValueObjects;

namespace Crud.DDD.Domain.Test.User
{
    public class UserTests
    {
        [Fact]
        public void Create_Should_ResturnValidUser()
        {
            var email = Email.Create("pouya@gmail.com");

            var user = Crud.DDD.Core.Aggregates.UserAggregate
                .User.Create("Kpouya96", "pouya", "karimian", email);

            Assert.NotEmpty(user.Id.ToString());
        }
    }
}
