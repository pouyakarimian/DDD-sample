using AutoMapper;
using Crud.DDD.Application.Features.User.Dtos;

namespace Crud.DDD.Application.Features.User.Mappings
{
    public class UserProfile : Profile
    {
        UserProfile()
        {
            CreateMap<DDD.Core.Aggregates.UserAggregate.User, UserDto>();
        }
    }
}
