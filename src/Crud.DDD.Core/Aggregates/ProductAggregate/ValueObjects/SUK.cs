using Crud.DDD.Core.Common.Exeptions;

namespace Crud.DDD.Core.Aggregates.ProductAggregate.ValueObjects
{
    public record SKU
    {
        public const int SKU_MIN_LENGTH = 8;
        public const int SKU_MAX_LENGTH = 12;
        private SKU()
        {

        }
        public string Value { get; private set; } = string.Empty;

        public SKU(string value)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(value, "SKU value can't be null");

            if (value.Length > SKU_MAX_LENGTH || value.Length < SKU_MIN_LENGTH)
                throw new BusinessException("Invalid SKU value");

            Value = value;
        }
    }
}
