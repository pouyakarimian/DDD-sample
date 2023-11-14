namespace Crud.DDD.Domian.Common
{
    public abstract class AggregateRoot<Tkey> : Entity<Tkey>
    {
        protected AggregateRoot(Tkey id) : base(id)
        {
        }

        public override bool Equals(object? obj)
        {
            return obj is Entity<Tkey> entity && Id.Equals(entity.Id);
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
