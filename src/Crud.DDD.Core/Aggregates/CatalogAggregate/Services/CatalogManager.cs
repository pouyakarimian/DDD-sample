using Crud.DDD.Core.Aggregates.CatalogAggregate.Repositories;
using Crud.DDD.Core.Common;
using Crud.DDD.Core.Common.Exeptions;
using System.Diagnostics.CodeAnalysis;

namespace Crud.DDD.Core.Aggregates.CatalogAggregate.Services
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

        public async Task<Catalog> AddAsync(Catalog catalog, CancellationToken cancellationToken)
        {
            var isExistingByName = await _catalogRepository
                .ExistingByName(catalog.Name, cancellationToken);

            if (isExistingByName)
                throw new BusinessException($"This catalog {catalog.Name} already saved");

            _catalogRepository.Add(catalog);

            await _unitOfWork.CommitAsync(cancellationToken);

            return catalog;
        }

        public async Task DeleteAsync([NotNull] Guid catalogId, CancellationToken cancellationToken)
        {
            var catalogEntity = await _catalogRepository
              .GetByIdAsync(catalogId, cancellationToken);

            if (catalogEntity is null)
                throw new NotFoundExeption(nameof(catalogEntity));

            _catalogRepository.Delete(catalogEntity);

            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task UpdateAsync(Catalog catalog, CancellationToken cancellationToken)
        {
            var catalogEntity = await _catalogRepository
              .GetByIdAsync(catalog.Id, cancellationToken);

            if (catalogEntity is null)
                throw new NotFoundExeption(nameof(catalogEntity));

            catalogEntity.Update(catalog.Name);

            _catalogRepository.Update(catalogEntity);

            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
