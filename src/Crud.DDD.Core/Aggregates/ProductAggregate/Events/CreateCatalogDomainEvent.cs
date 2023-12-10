using Crud.DDD.Core.Common;

namespace Crud.DDD.Core.Aggregates.ProductAggregate.Events
{
    public record CreateCatalogDomainEvent : IDomainEvent
    {
        public CreateCatalogDomainEvent(string name, Guid id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; private set; } = string.Empty;
        public Guid Id { get;private set; }
    }
}
