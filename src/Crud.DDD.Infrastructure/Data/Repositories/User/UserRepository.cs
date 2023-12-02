using Crud.DDD.Core.Aggregates.UserAggregate.Repositories;
using Crud.DDD.Infrastructure.Data.Context;

namespace Crud.DDD.Infrastructure.Data.Repositories.User
{
    public class UserRepository : BaseRepository<DDD.Core.Aggregates.UserAggregate.User, Guid>
        , IUserRepository
    {
        public UserRepository(ApplicationDBContext context) : base(context)
        {
        }

        public bool IsExistingByEmailOrUserName(string email, string userName, CancellationToken cancellationToken)
        => GetAllAsNoTracking()
            .Any(p => p.Email.Address.Equals(email)
            || p.UserName.Equals(userName));
    }
}
