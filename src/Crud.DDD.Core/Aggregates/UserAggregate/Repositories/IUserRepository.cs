using Crud.DDD.Core.Common;

namespace Crud.DDD.Core.Aggregates.UserAggregate.Repositories
{
    public interface IUserRepository : IBaseRepository<User, Guid>
    {
        bool IsExistingByEmailOrUserName(string email, string userName, CancellationToken cancellationToken);
        Task<(IQueryable<DDD.Core.Aggregates.UserAggregate.User> users, int count)> GetFilterQuery(string userName, string email, CancellationToken cancellationToken);
    }
}
