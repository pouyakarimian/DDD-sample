namespace Crud.DDD.Core.Aggregates.ProductAggregate.Services
{
    public interface IProductManager
    {
        Task<Product> AddProductAsync(Product product, CancellationToken cancellationToken);
        Task<Product> UpdateProductAsync(Product product, CancellationToken cancellationToken);
        Task<Product> DeleteProductAsync(Guid productId, CancellationToken cancellationToken);
    }
}
