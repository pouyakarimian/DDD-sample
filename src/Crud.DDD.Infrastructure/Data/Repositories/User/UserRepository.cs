using Crud.DDD.Core.Aggregates.UserAggregate.Repositories;
using Crud.DDD.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Crud.DDD.Infrastructure.Data.Repositories.User
{
    public class UserRepository : BaseRepository<DDD.Core.Aggregates.UserAggregate.User, Guid>
        , IUserRepository
    {
        public UserRepository(ApplicationDBContext context) : base(context)
        {
        }

        public async Task<bool> IsExistingByEmailOrUserName(string email, string userName, CancellationToken cancellationToken)
        => await (GetAllAsNoTracking())
            .AnyAsync(p => p.Email.Address.Equals(email)
            || p.UserName.Equals(userName), cancellationToken);
    }
}
