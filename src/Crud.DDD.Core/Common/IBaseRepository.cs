namespace Crud.DDD.Core.Common
{
    public interface IBaseRepository<TEntity, TKey>
        where TEntity : Entity<TKey> where TKey : notnull
    {
        IQueryable<TEntity> GetAllAsNoTracking();
        IQueryable<TEntity> GetAll();
        Task<TEntity?> GetByIdAsync(TKey key, CancellationToken cancellationToken);
        Task<TEntity?> GetByIdNoTrackingAsync(TKey key, CancellationToken cancellationToken);
        void Add(TEntity entity);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
