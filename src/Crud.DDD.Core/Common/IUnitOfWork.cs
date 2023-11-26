namespace Crud.DDD.Core.Common
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
    }
}
