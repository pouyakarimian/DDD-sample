using Crud.DDD.Core.Aggregates.UserAggregate.Repositories;
using Crud.DDD.Core.Common;
using Crud.DDD.Core.Common.Exeptions;
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
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var email = Email.Create(request.Email);

            var isExistingByEmail = await _userRepository
                .IsExistingByEmail(request.Email, cancellationToken);

            if (isExistingByEmail)
                throw new NotFoundExeption($"This email {request.Email} already used");

            var userDomain = Core.Aggregates.UserAggregate
                .User.Create(request.FirstName, request.LastName, email);

            _userRepository.Add(userDomain);

            await _unitOfWork.CommitAsync();
        }
    }
}
