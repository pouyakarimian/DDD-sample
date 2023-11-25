using MediatR;

namespace Crud.DDD.Application.Features.User.Commands
{
    public record CreateUserCommand : IRequest
    {
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string Email { get; init; }
    }

    public class CreateUserHandler : IRequestHandler<CreateUserCommand>
    {
        public CreateUserHandler()
        {

        }

        public Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
