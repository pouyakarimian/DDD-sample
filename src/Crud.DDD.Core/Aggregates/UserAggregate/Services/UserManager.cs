
using Crud.DDD.Core.Aggregates.UserAggregate.Repositories;
using Crud.DDD.Core.Common;
using Crud.DDD.Core.Common.Exeptions;

namespace Crud.DDD.Core.Aggregates.UserAggregate.Services
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserManager(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<User> AddAsync(User user, CancellationToken cancellationToken)
        {
            var isExistingByEmailOrUserName = await _userRepository
            .IsExistingByEmailOrUserName(user.Email.Address, user.UserName, cancellationToken);

            if (isExistingByEmailOrUserName)
                throw new NotFoundExeption($"This email {user.UserName} already used");

            _userRepository.Add(user);

            await _unitOfWork.CommitAsync();

            return user;
        }

        public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            var userEntity = await _userRepository
                .GetByIdAsync(user.Id, cancellationToken);

            if (userEntity is null)
                throw new NotFoundExeption($"{user.UserName} not founded");

            var updatedEntity = userEntity
                .Update(user.Id, user.UserName, user.LastName, user.LastName, user.Email);

            _userRepository.Update(updatedEntity);

            await _unitOfWork.CommitAsync();

            return updatedEntity;
        }
    }
}
