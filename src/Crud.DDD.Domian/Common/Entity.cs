namespace Crud.DDD.Domian.Common
{
    public abstract class Entity<TKey> where TKey : notnull
    {
        public Entity(TKey id)
        {
            Id = id;
        }
        public TKey Id { get; private set; }

    }
}
