using Crud.DDD.Core.Aggregates.UserAggregate.Events;
using Crud.DDD.Core.Common;
using Crud.DDD.Core.Common.ValueObjects;

namespace Crud.DDD.Core.Aggregates.UserAggregate
{
    public class User : AggregateRoot, ISoftDelete, IFullAudited
    {
        public User(Guid id) : base(id)
        {
        }

        private User(Guid id, string userName, string firstName, string lastName, Email email)
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = userName;
        }

        public string UserName { get; private set; } = string.Empty;
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public Email Email { get; private set; }
        public bool IsDeleted { get; set; }
        public Guid CreateUserId { get; set; }
        public Guid? DeleteUserId { get; set; }
        public Guid? ModifyUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? DeleteTime { get; set; }
        public DateTime? ModifyTime { get; set; }

        public static User Create(string userName, string firstName, string lastName, Email email)
        {
            ArgumentException.ThrowIfNullOrEmpty($"{nameof(firstName)} can't be null");

            ArgumentException.ThrowIfNullOrEmpty($"{nameof(userName)} can't be null");

            email = Email.Create(email.Address);

            var userId = Guid.NewGuid();

            var user = new User(userId, userName, firstName, lastName, email);

            user.RaiseDomainEvent(new CreateUserDomainEvent(userId, userName, firstName, lastName, email.Address));

            return user;
        }

        public User Update(Guid id, string userName, string firstName, string lastName, Email email)
        {
            ArgumentException.ThrowIfNullOrEmpty(firstName, $"{nameof(firstName)} can't be null");

            ArgumentException.ThrowIfNullOrEmpty(userName, $"{nameof(userName)} can't be null");

            ArgumentException.ThrowIfNullOrEmpty(id.ToString(), $"{nameof(id)} can't be null");

            ArgumentException.ThrowIfNullOrEmpty(email.Address, $"{nameof(email)} can't be null");

            email = Email.Create(email.Address);

            var user = new User(id, userName, firstName, lastName, email);

            user.RaiseDomainEvent(new UpdateUserDomainEvent(id, userName, firstName, lastName, email.Address));

            return user;
        }
    }
}
