using Crud.DDD.Core.Common;

namespace Crud.DDD.Core.Aggregates.ProductAggregate.Repositories
{
    public interface IProductRepository : IBaseRepository<Product, Guid>
    {
        Task<bool> ExistingByNameAsync(string name, CancellationToken cancellationToken);
        Task<Product?> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken);
    }
}
