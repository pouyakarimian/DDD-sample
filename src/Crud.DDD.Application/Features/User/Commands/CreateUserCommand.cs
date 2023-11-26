using Crud.DDD.Core.Aggregates.UserAggregate.Services;
using Crud.DDD.Core.Common.ValueObjects;
using FluentValidation;
using MediatR;

namespace Crud.DDD.Application.Features.User.Commands
{
    public record CreateUserCommand : IRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.Email)
               .NotEmpty()
               .EmailAddress()
               .MaximumLength(50);
        }
    }

    public class CreateUserHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserManager _userManager;

        public CreateUserHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var email = Email.Create(request.Email);

            var userDomain = Core.Aggregates.UserAggregate
                .User.Create(request.FirstName, request.LastName, email);

            await _userManager.AddAsync(userDomain, cancellationToken);
        }
    }
}
