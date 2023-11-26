
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
            var isExistingByEmail = await _userRepository
            .IsExistingByEmail(user.Email.Address, cancellationToken);

            if (isExistingByEmail)
                throw new NotFoundExeption($"This email {user.Email.Address} already used");

            _userRepository.Add(user);

            await _unitOfWork.CommitAsync();

            return user;
        }

        public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _userRepository.Update(user);

            await _unitOfWork.CommitAsync();

            return user;
        }
    }
}
