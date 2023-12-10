using Crud.DDD.Core.Aggregates.ProductAggregate.ValueObjects;
using Crud.DDD.Core.Common;

namespace Crud.DDD.Core.Aggregates.ProductAggregate.Events
{
    public record UpdateProductDomainEvent : IDomainEvent
    {
        public UpdateProductDomainEvent(Guid productId, string name, SKU sKU)
        {
            Id = productId;
            Name = name;
            SKU = sKU;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public SKU SKU { get; private set; } = null!;
    }
}
