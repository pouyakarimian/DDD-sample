namespace Crud.DDD.Core.Common
{
    public abstract class AggregateRoot<TKey> : Entity<TKey>
    {
        protected AggregateRoot(TKey id) : base(id)
        {
        }
        protected AggregateRoot(TKey id,
           DateTime modifyDate,
           bool isDeleted) : base(id, modifyDate, isDeleted)
        {
        }

        protected AggregateRoot(TKey id,
          DateTime modifyDate) : base(id, modifyDate)
        {
        }

        public override bool Equals(object? obj)
        {
            return obj is Entity<TKey> entity && Id.Equals(entity.Id);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
