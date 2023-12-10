namespace Crud.DDD.Core.Common
{
    public abstract class AggregateRoot : AggregateRoot<Guid>
    {
        protected AggregateRoot(Guid id) : base(id)
        {
        }
    }

    public abstract class AggregateRoot<TKey> : Entity<TKey> where TKey : notnull
    {

        protected AggregateRoot(TKey id) : base(id)
        {
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
