using Crud.DDD.Application.Features.User.Dtos;
using Crud.DDD.Core.Aggregates.UserAggregate.Repositories;
using Crud.DDD.Core.Common.Pagination;
using MediatR;

namespace Crud.DDD.Application.Features.User.Queries
{
    public record GetAllUsersQuery : PagingParamQuery<PagedResultDto<UserDto>>
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class GetAllUserQueryHandler : IRequestHandler<GetAllUsersQuery, PagedResultDto<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PagedResultDto<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var query = await _userRepository
            .GetFilterQuery(request.UserName, request.Email, cancellationToken);

            var count = query.count;

            return PagedResultDto<UserDto>
                .ToPagedList(query.users
                .Select(p => new UserDto
                {
                    Id = p.Id,
                    Email = p.Email.Address,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    UserName = p.UserName
                }),
            count,
            request.PageIndex,
            request.PageSize);
        }
    }
}
