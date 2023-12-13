using Crud.DDD.Core.Aggregates.ProductAggregate.Repositories;
using Crud.DDD.Infrastructure.Data.Context;

namespace Crud.DDD.Infrastructure.Data.Repositories.Product
{
    public class ProductRepository : BaseRepository<DDD.Core.Aggregates.ProductAggregate.Product, Guid>, IProductRepository
    {
        public ProductRepository(ApplicationDBContext context) : base(context)
        {
        }

        public  Task<bool> ExistingByNameAsync(string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public  Task<Core.Aggregates.ProductAggregate.Product?> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
