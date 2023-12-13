using Crud.DDD.Core.Common;
using Crud.DDD.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Crud.DDD.Infrastructure.Data.Repositories
{
    public class BaseRepository<TEntity, TKey> :
        IBaseRepository<TEntity, TKey>
        where TEntity : Entity<TKey> where TKey : notnull
    {
        protected readonly ApplicationDBContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(ApplicationDBContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public async Task AddAsync(TEntity entity,CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<TEntity> GetAllAsNoTracking()
        {
            return _dbSet
                .AsNoTracking();
        }

        public async Task<TEntity?> GetByIdNoTrackingAsync(TKey key, CancellationToken cancellationToken)
            => await (GetAllAsNoTracking())
            .FirstOrDefaultAsync(entity => entity.Id.Equals(key)
            , cancellationToken);

        public async Task<TEntity?> GetByIdAsync(TKey key, CancellationToken cancellationToken)
        => await (GetAll())
            .FirstOrDefaultAsync(p => p.Id.Equals(key), cancellationToken);

    }
}
