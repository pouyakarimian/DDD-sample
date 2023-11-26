using Crud.DDD.Core.Common;
using Crud.DDD.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Crud.DDD.Infrastructure.Data.Repositories
{
    public class BaseRepository<TEntity, TKey> :
        IBaseRepository<TEntity, TKey>, IDisposable
        where TEntity : Entity<TKey> where TKey : notnull
    {
        private readonly ApplicationDBContext _context;

        public BaseRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Add<TEntity>(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Remove<TEntity>(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Update<TEntity>(entity);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAllAsNoTracking()
        {
            return _context.Set<TEntity>()
                .AsNoTracking();
        }

        public async Task<TEntity> GetByIdNoTrackingAsync(TKey key, CancellationToken cancellationToken)
            => await _context
            .Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.Id.Equals(key)
            , cancellationToken);

        public Task<TEntity> GetByIdAsync(TKey key, CancellationToken cancellationToken)
        => _context.Set<TEntity>()
            .FirstOrDefaultAsync(p => p.Id.Equals(key), cancellationToken);

        #region IDisposable

        // To detect redundant calls.
        private bool _disposed;

        ~BaseRepository() => Dispose(false);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            // Dispose managed state (managed objects).
            if (disposing)
                _context.Dispose();

            _disposed = true;
        }
        #endregion
    }
}
