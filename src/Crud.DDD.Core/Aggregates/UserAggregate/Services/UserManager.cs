using Crud.DDD.Core.Aggregates.UserAggregate.Repositories;
using Crud.DDD.Core.Common;
using Crud.DDD.Core.Common.Exeptions;

namespace Crud.DDD.Core.Aggregates.UserAggregate.Services
{
    public class UserManager : IUserManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserManager(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<User> AddAsync(User user, CancellationToken cancellationToken)
        {

            var isExistingByEmailOrUserName = _userRepository
           .IsExistingByEmailOrUserName
           (user.Email.Address, user.UserName, cancellationToken);

            if (isExistingByEmailOrUserName)
                throw new BusinessException($"This email {user.UserName} already used");

            _userRepository.Add(user);

            await _unitOfWork.CommitAsync(cancellationToken);

            return user;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _userRepository.GetByIdAsync(id, cancellationToken);

            if (entity is null)
                throw new NotFoundExeption(nameof(entity));

            _userRepository.Delete(entity);

            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            var userEntity = await _userRepository
                .GetByIdAsync(user.Id, cancellationToken);

            if (userEntity is null)
                throw new NotFoundExeption($"{user.UserName}");

            userEntity.Update(userEntity.Id, user.UserName, user.FirstName
               , user.Email, user.LastName);

            _userRepository.Update(userEntity);

            await _unitOfWork.CommitAsync(cancellationToken);

            return user;
        }
    }
}
