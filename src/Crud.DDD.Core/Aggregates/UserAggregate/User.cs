using Crud.DDD.Core.Aggregates.UserAggregate.Events;
using Crud.DDD.Core.Common;
using Crud.DDD.Core.Common.Exeptions;
using Crud.DDD.Core.Common.ValueObjects;

namespace Crud.DDD.Core.Aggregates.UserAggregate
{
    public class User : AggregateRoot, ISoftDelete, IFullAudited
    {
        public User(Guid id) : base(id)
        {
        }

        private User(Guid id, string firstName, string lastName, Email email)
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

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

        public static User Create(string firstName, string lastName, Email email)
        {
            if (string.IsNullOrEmpty(firstName))
                ArgumentException.ThrowIfNullOrEmpty($"{nameof(firstName)} can't be null");

            var userId = Guid.NewGuid();

            var user = new User(userId, firstName, lastName, email);

            user.RaiseDomainEvent(new CreateUserDomainEvent(userId, firstName, lastName, email.Address));

            return user;
        }

        public static User Update(Guid id, string firstName, string lastName, Email email)
        {
            if (string.IsNullOrEmpty(firstName))
                ArgumentException.ThrowIfNullOrEmpty($"{nameof(firstName)} can't be null");

            if (string.IsNullOrEmpty(id.ToString()))
                throw new BusinessException($"{nameof(id)} can't be null");

            return new User(id, firstName, lastName, email);
        }
    }
}
