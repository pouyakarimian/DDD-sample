using Crud.DDD.Core.Common;

namespace Crud.DDD.Core.Aggregates.UserAggregate.Events
{
    public sealed record UpdateUserDomainEvent : IDomainEvent
    {
        private UpdateUserDomainEvent()
        {

        }

        public UpdateUserDomainEvent(Guid id, string userName, string firstName,
            string lastName, string email)
        {
            Id = id;
            FirstName = firstName;
            UserName = userName;
            LastName = lastName;
            Email = email;
        }

        public Guid Id { get; private set; }
        public string UserName { get; private set; } = string.Empty;
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
    }
}
