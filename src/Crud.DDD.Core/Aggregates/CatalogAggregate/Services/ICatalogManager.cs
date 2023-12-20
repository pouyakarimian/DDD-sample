namespace Crud.DDD.Core.Aggregates.CatalogAggregate.Services
{
    public interface ICatalogManager
    {
        Task<Catalog> AddAsync(Catalog catalog, CancellationToken cancellationToken);
        Task UpdateAsync(Catalog catalog, CancellationToken cancellationToken);
        Task DeleteAsync(Guid catalogId, CancellationToken cancellationToken);
    }
}
