using Crud.DDD.Core.Aggregates.ProductAggregate.Entities;

namespace Crud.DDD.Core.Aggregates.ProductAggregate.Services
{
    public interface ICatalogManager
    {
        Task<Catalog> AddCatalogAsync(Catalog catalog, CancellationToken cancellationToken);
        Task<Catalog> UpdateCatalogAsync(Catalog catalog, CancellationToken cancellationToken);
        Task<Catalog> DeleteCatalogAsync(Guid catalogId, CancellationToken cancellationToken);
        Task<Product> AddProductAsync(Product product, CancellationToken cancellationToken);
        Task<Product> UpdateProductAsync(Product product, CancellationToken cancellationToken);
        Task<Product> DeleteProductAsync(Guid productId, CancellationToken cancellationToken);
    }
}
