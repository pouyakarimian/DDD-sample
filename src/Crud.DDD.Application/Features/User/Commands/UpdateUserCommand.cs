using Crud.DDD.Core.Aggregates.UserAggregate.Services;
using Crud.DDD.Core.Common.ValueObjects;
using FluentValidation;
using MediatR;

namespace Crud.DDD.Application.Features.User.Commands
{
    public record UpdateUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

    }
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(p => p.Id)
            .NotEmpty()
            .NotNull();

            RuleFor(p => p.FirstName)
               .NotEmpty()
               .MaximumLength(50);

            RuleFor(p => p.UserName)
               .NotEmpty()
               .MaximumLength(50);

            RuleFor(p => p.Email)
               .NotEmpty()
               .EmailAddress()
               .MaximumLength(50);
        }
    }
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserManager _userManager;

        public UpdateUserCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var email = Email.Create(request.Email);

            var userEntity = DDD.Core.Aggregates.UserAggregate.User
                .Update(request.Id, request.UserName,
                request.FirstName, request.LastName, email);

            await _userManager.UpdateAsync(userEntity, cancellationToken);
        }
    }
}
