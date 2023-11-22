using Crud.DDD.Core.Aggregates.UserAggregate.Events;
using MediatR;

namespace Crud.DDD.Application.Features.User.Events
{
    public sealed class CreateUserEventHandler : INotificationHandler<CreateUserDomainEvent>
    {
        public Task Handle(CreateUserDomainEvent notification, CancellationToken cancellationToken)
        {
            //Fetch from DB then send
            //send email
            throw new NotImplementedException();
        }
    }
}
