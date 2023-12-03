namespace Crud.DDD.Core.Common
{
    public abstract class Entity<TKey> where TKey : notnull
    {
        protected Entity(TKey id)
        {
            Id = id;
        }

        public TKey Id { get; protected set; }

    }
}
