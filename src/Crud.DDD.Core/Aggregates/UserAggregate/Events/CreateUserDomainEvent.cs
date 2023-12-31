﻿using Crud.DDD.Core.Common;

namespace Crud.DDD.Core.Aggregates.UserAggregate.Events
{
    public sealed record CreateUserDomainEvent : IDomainEvent
    {
        private CreateUserDomainEvent()
        {

        }

        public CreateUserDomainEvent(Guid id, string userName, string firstName,
            string lastName, string email)
        {
            Id = id;
            FirstName = firstName;
            UserName = userName;
            LastName = lastName;
            Email = email;
        }

        public Guid Id { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string UserName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
    }
}
