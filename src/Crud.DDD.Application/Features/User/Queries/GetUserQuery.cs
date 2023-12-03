using Crud.DDD.Application.Features.User.Dtos;
using Crud.DDD.Core.Aggregates.UserAggregate.Repositories;
using Crud.DDD.Core.Common.Exeptions;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Crud.DDD.Application.Features.User.Queries
{
    public record GetUserQuery : IRequest<UserDto>
    {
        [Required]
        public required Guid UserId { get; init; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {

            var user = await _userRepository
                .GetByIdNoTrackingAsync(request.UserId, cancellationToken);

            if (user is null)
                throw new NotFoundExeption(nameof(user));

            return new UserDto
            {
                Email = user.Email.Address,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id
            };

        }
    }


}
