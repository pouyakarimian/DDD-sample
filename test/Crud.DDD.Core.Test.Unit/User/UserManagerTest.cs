using Crud.DDD.Core.Aggregates.UserAggregate.Repositories;
using Crud.DDD.Core.Aggregates.UserAggregate.Services;
using Crud.DDD.Core.Common;
using Crud.DDD.Core.Common.Exeptions;
using Crud.DDD.Core.Common.ValueObjects;
using Moq;

namespace Crud.DDD.Core.Test.Unit.User
{
    public class UserManagerTest
    {
        [Fact]
        public async Task AddAsync_ShouldSaveAndReturnValidUser_WhenAddNewUser()
        {
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
            var cancellationToken = new CancellationToken();
            var userManager = new UserManager(mockUnitOfWork.Object, userRepository.Object);

            var email = Email.Create("pouya@gmail.com");

            var user = DDD.Core.Aggregates.UserAggregate.User.Create("pouya", "pouya", "karimin", email);

            userRepository.Setup(p => p.Add(user)).Verifiable();

            var userResult = await userManager.AddAsync(user, cancellationToken);

            Assert.Equal(user, userResult);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowBusinessException_WhenUserAlreadyExsit()
        {
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
            var cancellationToken = new CancellationToken();
            var userManager = new UserManager(mockUnitOfWork.Object, userRepository.Object);

            var email = Email.Create("pouya@gmail.com");

            var user = DDD.Core.Aggregates.UserAggregate.User.Create("pouya", "pouya", "karimin", email);

            userRepository.Setup(p => p.Add(user)).Verifiable();

            userRepository.Setup(p => p.IsExistingByEmailOrUserName(email.Address, "pouya", cancellationToken))
                .Returns(() => true);

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await userManager.AddAsync(user, cancellationToken);
            });
        }

    }
}
