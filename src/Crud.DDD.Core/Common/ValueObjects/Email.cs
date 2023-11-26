using Crud.DDD.Core.Common.Exeptions;
using Crud.DDD.Core.Shared;

namespace Crud.DDD.Core.Common.ValueObjects
{
    public record Email
    {
        private Email()
        {

        }
        public Email(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new BusinessException($"{nameof(email)} can't be null");

            if (!RegexPatterns.EmailIsValid.IsMatch(email))
                throw new BusinessException("Invaild email");

            Address = email;
        }
        public string Address { get; private set; } = string.Empty;

        public static Email Create(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new BusinessException("Invaild email");

            if (!RegexPatterns.EmailIsValid.IsMatch(email))
                throw new BusinessException("Invaild email");

            return new Email(email);
        }
    }
}
