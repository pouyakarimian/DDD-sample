namespace Crud.DDD.Core.Common
{
    public interface IEntity
    {
        public IReadOnlyList<IDomainEvent> DomainEvents { get; }
       public void ClearDomainEvents();
    }
    public abstract class Entity<TKey>: IEntity where TKey : notnull
    {
        private readonly List<IDomainEvent> _events = new();
        public IReadOnlyList<IDomainEvent> DomainEvents { get { return _events.ToList(); } }

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _events.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _events.Clear();
        }

        protected Entity(TKey id)
        {
            Id = id;
        }

        public TKey Id { get; protected set; }

    }
}
