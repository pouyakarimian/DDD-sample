using Crud.DDD.Core.Aggregates.UserAggregate;

namespace Crud.DDD.Core.Common
{
    public record CurrentUserInfo
    {
        public CurrentUserInfo(Guid id, string email, string name, string userName)
        {
            Id = id;
            Email = email;
            Name = name;
            UserName = userName;
        }

        public Guid Id { get; private set; }
        public string Email { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public string UserName { get; private set; } = string.Empty;
    }

    public interface ICurrentUserService
    {
        Task<CurrentUserInfo> GetCurrentUserAsync();
    }
}
