using Crud.DDD.Core.Aggregates.ProductAggregate.Repositories;
using Crud.DDD.Core.Common;
using Crud.DDD.Core.Common.Exeptions;
using System.Diagnostics.CodeAnalysis;

namespace Crud.DDD.Core.Aggregates.ProductAggregate.Services
{
    public class ProductManager : IProductManager
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductManager(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken)
        {
            var productEntity = await _productRepository
               .GetByIdAsync(product.Id, cancellationToken);

            if (productEntity is null)
                throw new NotFoundExeption(nameof(productEntity));

            _productRepository.Add(product);

            await _unitOfWork.CommitAsync(cancellationToken);

            return product;
        }

        public async Task DeleteAsync([NotNull] Guid productId, CancellationToken cancellationToken)
        {
            var productEntity = await _productRepository
             .GetByIdAsync(productId, cancellationToken);

            if (productEntity is null)
                throw new NotFoundExeption(nameof(productEntity));

            _productRepository.Delete(productEntity);
            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            var productEntity = await _productRepository
                 .GetByIdAsync(product.Id, cancellationToken);

            if (productEntity is null)
                throw new NotFoundExeption(nameof(product));

            productEntity.Update(product.Id, product.CatalogId, product.Name, product.SKU.Value);

            _productRepository.Update(productEntity);

            await _unitOfWork.CommitAsync(cancellationToken);

            return productEntity;
        }
    }
}
