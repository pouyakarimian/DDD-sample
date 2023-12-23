using Crud.DDD.Core.Aggregates.ProductAggregate.Events;
using MediatR;

namespace Crud.DDD.Application.Features.Product.Events
{
    public sealed class CreatedProductEventHandler : INotificationHandler<CreateProductDomainEvent>
    {
        public CreatedProductEventHandler() { }

        public Task Handle(CreateProductDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
