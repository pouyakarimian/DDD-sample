using Crud.DDD.Core.Aggregates.CatalogAggregate.Repositories;
using Crud.DDD.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Crud.DDD.Infrastructure.Data.Repositories.Catalog
{
    public class CatalogRepository : BaseRepository<Core.Aggregates.CatalogAggregate.Catalog, Guid>,
        ICatalogRepository
    {
        public CatalogRepository(ApplicationDBContext context) : base(context)
        {
        }

        public async Task<bool> ExistingByName(string name, CancellationToken cancellationToken)
        => await (GetAllAsNoTracking()
            .AnyAsync(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase), cancellationToken));

        public async Task<Core.Aggregates.CatalogAggregate.Catalog?> GetCatalogWithSingleProductByProductIdAndCatalogId(Guid productId, Guid catalogId, CancellationToken cancellationToken)
          => await (GetAll()
                .Include(p => p.Products.Where(c=>c.Id.Equals(productId)))                
                .FirstOrDefaultAsync(p =>p.Id.Equals(catalogId), cancellationToken));
    }
}
