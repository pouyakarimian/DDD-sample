using Crud.DDD.Core.Aggregates.CatalogAggregate.Repositories;
using Crud.DDD.Core.Common;
using Crud.DDD.Core.Common.Exeptions;

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

       

        public Task<Catalog> DeleteCatalogAsync(Guid catalogId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Catalog> UpdateCatalogAsync(Catalog catalog, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
