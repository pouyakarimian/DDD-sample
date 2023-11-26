using Crud.DDD.Core.Common;

namespace Crud.DDD.Core.Aggregates.UserAggregate.Repositories
{
    public interface IUserRepository : IBaseRepository<User, Guid>
    {
        Task<bool> IsExistingByEmail(string email, CancellationToken cancellationToken);
    }
}
