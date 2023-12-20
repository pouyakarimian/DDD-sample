using Crud.DDD.Core.Aggregates.CatalogAggregate.Events;
using Crud.DDD.Core.Aggregates.ProductAggregate;
using Crud.DDD.Core.Common;

namespace Crud.DDD.Core.Aggregates.CatalogAggregate
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

        public Catalog Update(string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, $"{nameof(name)} can't be null");
            var normalizedName = name.ToUpper();
            var catalog = new Catalog(this.Id, name, normalizedName, 0);
            catalog.RaiseDomainEvent(new UpdateCatalogDomainEvent(name, this.Id));
            return catalog;
        }
    }
}
