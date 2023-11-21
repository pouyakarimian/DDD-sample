using Crud.DDD.Core.Common;
using Crud.DDD.Core.Common.Exeptions;
using Crud.DDD.Core.Common.ValueObjects;

namespace Crud.DDD.Core.Aggregates.UserAggregate
{
    public class User : AggregateRoot<Guid>
    {
        private User(Guid id, string firstName, string lastName, Email email)
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        private User(Guid id, string firstName, string lastName, Email email,
          DateTime modifyDate)
          : base(id, modifyDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public Email Email { get; private set; }

        public User Create(string firstName, string lastName, Email email)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new BusinessException($"{nameof(firstName)} can't be null");

            var userId = Guid.NewGuid();

            return new User(userId, firstName, lastName, email);
        }

        public User Update(Guid id, string firstName, string lastName, Email email)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new BusinessException($"{nameof(firstName)} can't be null");

            if (string.IsNullOrEmpty(id.ToString()))
                throw new BusinessException($"{nameof(id)} can't be null");

            var modifyDate = DateTime.Now;

            return new User(id, firstName, lastName, email, modifyDate);
        }

        public User Deleted()
        {
            this.IsDeleted = true;

            return this;
        }
    }
}
