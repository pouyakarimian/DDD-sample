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
        private readonly List<IDomainEvent> _events = new();

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

        public IReadOnlyList<IDomainEvent> DomainEvents { get { return _events.ToList(); } }
        protected void ClearDomainEvents()
        {
            _events.Clear();
        }
        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _events.Add(domainEvent);
        }
    }
}
