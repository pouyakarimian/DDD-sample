using Crud.DDD.Core.Aggregates.UserAggregate.Repositories;
using Crud.DDD.Core.Shared.Extensions;
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

        public bool IsExistingByEmailOrUserName(string email, string userName, CancellationToken cancellationToken)
        => GetAllAsNoTracking()
            .Any(p => p.Email.Address.Equals(email)
            || p.UserName.Equals(userName));

        public async Task<(IQueryable<DDD.Core.Aggregates.UserAggregate.User> users, int count)>
            GetFilterQuery(string userName, string email, CancellationToken cancellationToken)
        {
            var query = GetAllAsNoTracking()
                .WhereIf(!string.IsNullOrEmpty(userName), p => p.UserName.Contains(userName))
                .WhereIf(!string.IsNullOrEmpty(email), p => p.Email.Address.Contains(email))
                .OrderBy(p => p.Id)
                .ThenBy(p => p.UserName);

            var count = await query.CountAsync(cancellationToken);

            return (query, count);
        }
    }
}
