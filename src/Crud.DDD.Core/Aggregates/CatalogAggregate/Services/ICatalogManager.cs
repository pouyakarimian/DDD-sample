namespace Crud.DDD.Core.Aggregates.CatalogAggregate.Services
{
    public interface ICatalogManager
    {
        Task<Catalog> AddCatalogAsync(Catalog catalog, CancellationToken cancellationToken);
        Task<Catalog> UpdateCatalogAsync(Catalog catalog, CancellationToken cancellationToken);
        Task<Catalog> DeleteCatalogAsync(Guid catalogId, CancellationToken cancellationToken);
    }
}
