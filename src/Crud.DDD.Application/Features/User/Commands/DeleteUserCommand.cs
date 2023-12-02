using Crud.DDD.Core.Aggregates.UserAggregate.Services;
using FluentValidation;
using MediatR;

namespace Crud.DDD.Application.Features.User.Commands
{
    public record DeleteUserCommand : IRequest
    {
        public Guid UserId { get; private set; }

        public DeleteUserCommand(Guid userId)
        {
            UserId = userId;
        }
        private DeleteUserCommand() { }
    }

    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(p => p.UserId)
                .NotEmpty()
                .NotNull();
        }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserManager _userManager;

        public DeleteUserCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _userManager.DeleteAsync(request.UserId, cancellationToken);
        }
    }
}
