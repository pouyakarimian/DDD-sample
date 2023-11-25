using Crud.DDD.Core.Aggregates.UserAggregate.Events;
using Crud.DDD.Core.Common;
using Crud.DDD.Core.Common.Exeptions;
using Crud.DDD.Core.Common.ValueObjects;

namespace Crud.DDD.Core.Aggregates.UserAggregate
{
    public class User : AggregateRoot, ISoftDelete, IFullAudited
    {
        public User(Guid id):base(id)
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

        public User Create(string firstName, string lastName, Email email)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new BusinessException($"{nameof(firstName)} can't be null");

            var userId = Guid.NewGuid();

            RaiseDomainEvent(new CreateUserDomainEvent(userId, firstName, lastName, email.Address));

            return new User(userId, firstName, lastName, email);
        }

        public User Update(Guid id, string firstName, string lastName, Email email)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new BusinessException($"{nameof(firstName)} can't be null");

            if (string.IsNullOrEmpty(id.ToString()))
                throw new BusinessException($"{nameof(id)} can't be null");

            return new User(id, firstName, lastName, email);
        }
    }
}
