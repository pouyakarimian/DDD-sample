using Crud.DDD.Core.Aggregates.ProductAggregate.Entities;
using Crud.DDD.Core.Common;

namespace Crud.DDD.Core.Aggregates.ProductAggregate.Repositories
{
    public interface ICatalogRepository : IBaseRepository<Catalog, Guid>
    {
        Task<bool> ExistingByName(string name, CancellationToken cancellationToken);
        Task<Catalog?> GetProductByProductIdAndCatalogId(Guid productId, Guid catalogId, CancellationToken cancellationToken);
    }
}
