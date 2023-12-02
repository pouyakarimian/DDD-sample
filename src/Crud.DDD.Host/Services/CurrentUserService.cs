using Crud.DDD.Core.Common;

namespace Crud.DDD.Host.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public async Task<CurrentUserInfo> GetCurrentUserAsync()
        {
            return await Task.FromResult<CurrentUserInfo>(new CurrentUserInfo(Guid.NewGuid(), "pouya@gmail.com", "pouya", "pouya1996"));
        }
    }
}
