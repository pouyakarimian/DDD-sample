namespace Crud.DDD.Core.Aggregates.ProductAggregate.Services
{
    public interface IProductManager
    {
        Task<Product> AddAsync(Product product, CancellationToken cancellationToken);
        Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken);
        Task DeleteAsync(Guid productId, CancellationToken cancellationToken);
    }
}
