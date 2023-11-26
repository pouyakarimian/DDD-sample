using Crud.DDD.Core.Aggregates.UserAggregate.Repositories;

namespace Crud.DDD.Infrastructure.Data.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        public Core.Aggregates.UserAggregate.User Add(Core.Aggregates.UserAggregate.User entity)
        {
            throw new NotImplementedException();
        }

        public Core.Aggregates.UserAggregate.User Delete(Core.Aggregates.UserAggregate.User entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Core.Aggregates.UserAggregate.User> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Core.Aggregates.UserAggregate.User> GetByIdAsync(Guid key, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Core.Aggregates.UserAggregate.User Update(Core.Aggregates.UserAggregate.User entity)
        {
            throw new NotImplementedException();
        }
    }
}
