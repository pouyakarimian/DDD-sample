using Crud.DDD.Core.Aggregates.ProductAggregate.Entities;
using Crud.DDD.Core.Aggregates.ProductAggregate.Events;
using Crud.DDD.Core.Common;
using Crud.DDD.Core.Common.Exeptions;

namespace Crud.DDD.Core.Aggregates.ProductAggregate
{
    public class Catalog : AggregateRoot, IFullAudited, ISoftDelete
    {
        private Catalog(Guid id) : base(id)
        {
        }

        private Catalog(Guid id, string name, string normalizedName, int productCount)
            : base(id)
        {
            Name = name;
            NormalizedName = normalizedName;
            ProductCount = productCount;
        }

        #region Auditing Fields
        public Guid CreateUserId { get; set; }
        public Guid? ModifyUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? ModifyTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteTime { get; set; }
        public Guid? DeleteUserId { get; set; }
        #endregion

        #region navigations
        private List<Product> _products = new();
        public virtual IReadOnlyCollection<Product> Products => _products.ToList();

        #endregion

        public string Name { get; private set; } = string.Empty;
        public string NormalizedName { get; private set; } = string.Empty;
        public int ProductCount { get; private set; }

        public static Catalog Create(string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, $"{nameof(name)} can't be null");

            var catalogId = Guid.NewGuid();
            var normalizedName = name.ToUpper();
            var catalog = new Catalog(catalogId, name, normalizedName, 0);
            catalog.RaiseDomainEvent(new CreateCatalogDomainEvent(name, catalogId));
            return catalog;
        }
        public Product CreateProduct(string name, string skuCode)
        {
            var product = Product.Create(this.Id, name, skuCode);
            _products.Add(product);
            return product;
        }
        public void RemoveProduct(Guid productId)
        {
            var product = _products
                .FirstOrDefault(p => p.Id.Equals(productId));

            if (product is null)
                throw new NotFoundExeption(nameof(product));

            _products.Remove(product);
        }
    }
}
