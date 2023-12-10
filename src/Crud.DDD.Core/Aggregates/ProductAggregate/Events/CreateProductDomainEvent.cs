using Crud.DDD.Core.Aggregates.ProductAggregate.ValueObjects;
using Crud.DDD.Core.Common;

namespace Crud.DDD.Core.Aggregates.ProductAggregate.Events
{
    public record CreateProductDomainEvent:IDomainEvent
    {
        public CreateProductDomainEvent(string name, SKU sKU)
        {
            Name = name;
            SKU = sKU;
        }

        public string Name { get; private set; } = string.Empty;
        public SKU SKU { get; private set; } = null!;
    }
}
