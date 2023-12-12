using Crud.DDD.Core.Aggregates.UserAggregate.Events;
using Crud.DDD.Core.Aggregates.UserAggregate.Repositories;
using Crud.DDD.Core.Common.Exeptions;
using MediatR;

namespace Crud.DDD.Application.Features.User.Events
{
    public sealed class CreateUserEventHandler : INotificationHandler<CreateUserDomainEvent>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserEventHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(CreateUserDomainEvent notification, CancellationToken cancellationToken)
        {
            var user = await _userRepository
                .GetByIdNoTrackingAsync(notification.Id, cancellationToken);

            if (user is null)
                throw new NotFoundExeption(nameof(user));
            //send email
        }
    }
}
