namespace Crud.DDD.Core.Common
{
    public abstract class Entity<TKey> where TKey : notnull
    {
        private readonly List<IDomainEvent> _events = new();
        public IReadOnlyList<IDomainEvent> DomainEvents { get { return _events.ToList(); } }
        protected void ClearDomainEvents()
        {
            _events.Clear();
        }
        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _events.Add(domainEvent);
        }

        protected Entity(TKey id)
        {
            Id = id;
        }

        public TKey Id { get; protected set; }

    }
}
