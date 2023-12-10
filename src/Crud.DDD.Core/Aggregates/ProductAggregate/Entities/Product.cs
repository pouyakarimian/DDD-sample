using Crud.DDD.Core.Aggregates.ProductAggregate.Events;
using Crud.DDD.Core.Aggregates.ProductAggregate.ValueObjects;
using Crud.DDD.Core.Common;

namespace Crud.DDD.Core.Aggregates.ProductAggregate.Entities
{
    public class Product : Entity<Guid>, IFullAudited, ISoftDelete
    {
        public Product(Guid id) : base(id)
        {
        }

        public Product(Guid id, Guid catalogId, string name, SKU sKU) : base(id)
        {
            Name = name;
            SKU = sKU;
            CatalogId = catalogId;
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

        public string Name { get; private set; } = string.Empty;
        public SKU SKU { get; private set; } = null!;
        public Guid CatalogId { get; private set; }
        public static Product Create(Guid catalogId, string name, string skuCode)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(skuCode, nameof(skuCode));
            ArgumentException.ThrowIfNullOrEmpty(catalogId.ToString(), nameof(skuCode));

            var productId = Guid.NewGuid();
            var sku = new Crud.DDD.Core.Aggregates.ProductAggregate.ValueObjects.SKU(skuCode);
            var product = new Product(productId, catalogId, name, sku);
            product.RaiseDomainEvent(new CreateProductDomainEvent(name, sku));

            return product;
        }

        public Product update(Guid Id, Guid catalogId, string name, string skuCode)
        {
            ArgumentException.ThrowIfNullOrEmpty(Id.ToString(), nameof(Id));
            ArgumentException.ThrowIfNullOrEmpty(catalogId.ToString(), nameof(skuCode));
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(skuCode, nameof(skuCode));

            var sku = new Crud.DDD.Core.Aggregates.ProductAggregate.ValueObjects.SKU(skuCode);
            this.CatalogId = catalogId;
            this.Id = Id;
            this.Name = name;
            this.SKU = sku;

            this.RaiseDomainEvent(new UpdateProductDomainEvent(Id, name, sku));

            return this;
        }

    }
}
