using Crud.DDD.Core.Aggregates.ProductAggregate.Entities;
using Crud.DDD.Core.Aggregates.ProductAggregate.Repositories;
using Crud.DDD.Core.Common;
using Crud.DDD.Core.Common.Exeptions;

namespace Crud.DDD.Core.Aggregates.ProductAggregate.Services
{
    public class CatalogManager : ICatalogManager
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CatalogManager(ICatalogRepository catalogRepository, IUnitOfWork unitOfWork)
        {
            _catalogRepository = catalogRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Catalog> AddCatalogAsync(Catalog catalog, CancellationToken cancellationToken)
        {
            var isExistingByName = await _catalogRepository
                .ExistingByName(catalog.Name, cancellationToken);

            if (isExistingByName)
                throw new BusinessException($"This catalog {catalog.Name} already saved");

            _catalogRepository.Add(catalog);

            await _unitOfWork.CommitAsync(cancellationToken);

            return catalog;
        }

        public async Task<Product> AddProductAsync(Product product, CancellationToken cancellationToken)
        {
            var catalog = await _catalogRepository
                .GetByIdAsync(product.CatalogId, cancellationToken);

            if (catalog is null)
                throw new NotFoundExeption(nameof(catalog));

            product = catalog.CreateProduct(product.Name, product.SKU.Value);

            await _unitOfWork.CommitAsync(cancellationToken);

            return product;
        }

        public Task<Catalog> DeleteCatalogAsync(Guid catalogId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Product> DeleteProductAsync(Guid productId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Catalog> UpdateCatalogAsync(Catalog catalog, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> UpdateProductAsync(Product product, CancellationToken cancellationToken)
        {
            var catalogEntity = await _catalogRepository
                .GetProductByProductIdAndCatalogId(product.CatalogId, product.Id, cancellationToken);

            if (catalogEntity is null)
                throw new NotFoundExeption(nameof(catalogEntity));

            var productEntity = catalogEntity.UpdateProduct(product.Id, product.Name, product.SKU.Value);

            await _unitOfWork.CommitAsync(cancellationToken);

            return productEntity;
        }
    }
}
