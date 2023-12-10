using Crud.DDD.Core.Common.Exeptions;

namespace Crud.DDD.Core.Aggregates.OrderAggregate.ValueObjects
{
    public record Money
    {
        public Money(decimal amount, string currency = "USD", decimal exchangeRate = 0)
        {
            Validate();
            Amount = amount;
            Currency = currency;
            ExchangeRate = exchangeRate;
        }

        public decimal Amount { get; private set; }
        public string Currency { get; private set; } = string.Empty;
        public decimal ExchangeRate { get; private set; }

        public void Validate()
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(Amount));

            if (Amount < 0)
                throw new BusinessException($"{nameof(Amount)} can't by lower than zero");
        }
    }
}
