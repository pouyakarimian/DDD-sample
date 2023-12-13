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
        Mock<IUnitOfWork> unitOfWork;
        Mock<IUserRepository> userRepository;
        UserManager userManager;

        public UserManagerTest()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            userRepository = new Mock<IUserRepository>();
            userManager = new UserManager(unitOfWork.Object, userRepository.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldSaveAndReturnValidUser_WhenAddNewUser()
        {
            var email = Email.Create("pouya@gmail.com");

            var user = DDD.Core.Aggregates.UserAggregate.User.Create("pouya", "pouya", "karimin", email);

            userRepository.Setup(p => p.IsExistingByEmailOrUserName(email.Address, "pouya", CancellationToken.None))
                .Returns(false);

            unitOfWork.Setup(p => p.CommitAsync(CancellationToken.None))
               .Returns(Task.CompletedTask);

            userRepository.Setup(p => p.Add(user));

            var userResult = await userManager.AddAsync(user, CancellationToken.None);

            Assert.Equal(user, userResult);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowBusinessException_WhenUserAlreadyExsit()
        {
            var email = Email.Create("pouya@gmail.com");

            var user = DDD.Core.Aggregates.UserAggregate.User.Create("pouya", "pouya", "karimin", email);

            userRepository.Setup(p => p.Add(user)).Verifiable();

            userRepository.Setup(p => p.IsExistingByEmailOrUserName(email.Address, "pouya", CancellationToken.None))
                .Returns(() => true);

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await userManager.AddAsync(user, CancellationToken.None);
            });
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateUserAndReturn_WhenUserIsValid()
        {
            var userId = Guid.NewGuid();
            var email = Email.Create("pouya@gmail.com");
            var user = DDD.Core.Aggregates.UserAggregate
                .User.Update(userId, "pouya", "pouya", "karimian", email);

            unitOfWork.Setup(p => p.CommitAsync(CancellationToken.None))
                .Returns(Task.CompletedTask);
            userRepository.Setup(p => p.GetByIdAsync(userId, CancellationToken.None))
                .Returns(async () =>
                {
                    await Task.FromResult(user);
                });
            userRepository.Setup(p => p.Update(user));

            var result = await userManager.UpdateAsync(user, CancellationToken.None);
            Assert.Equal(result, user);
        }


    }
}
