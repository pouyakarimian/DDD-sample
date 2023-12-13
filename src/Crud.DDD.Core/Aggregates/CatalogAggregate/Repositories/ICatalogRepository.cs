using Crud.DDD.Core.Common;

namespace Crud.DDD.Core.Aggregates.CatalogAggregate.Repositories
{
    public interface ICatalogRepository : IBaseRepository<Catalog, Guid>
    {
        Task<bool> ExistingByName(string name, CancellationToken cancellationToken);
        Task<Catalog?> GetCatalogWithSingleProductByProductIdAndCatalogId(Guid productId, Guid catalogId, CancellationToken cancellationToken);
    }
}
