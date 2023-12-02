using Crud.DDD.Core.Aggregates.UserAggregate.Repositories;

namespace Crud.DDD.Core.Common
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync(CancellationToken cancellationToken);
    }
}
