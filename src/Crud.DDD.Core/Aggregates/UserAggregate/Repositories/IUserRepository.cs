using Crud.DDD.Core.Common;

namespace Crud.DDD.Core.Aggregates.UserAggregate.Repositories
{
    public interface IUserRepository : IBaseRepository<User, Guid>
    {
        Task<bool> IsExistingByEmailOrUserName(string email, string userName, CancellationToken cancellationToken);
    }
}
