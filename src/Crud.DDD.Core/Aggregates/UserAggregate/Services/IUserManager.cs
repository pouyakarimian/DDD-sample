namespace Crud.DDD.Core.Aggregates.UserAggregate.Services
{
    public interface IUserManager
    {
        Task<User> AddAsync(User user, CancellationToken cancellationToken);
        Task<User> UpdateAsync(User user, CancellationToken cancellationToken);
    }
}
