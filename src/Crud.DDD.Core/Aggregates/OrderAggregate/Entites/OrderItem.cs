using Crud.DDD.Core.Common;

namespace Crud.DDD.Core.Aggregates.Order.Entites
{
    public class OrderItem : Entity<Guid>
    {
        public OrderItem(Guid id) : base(id)
        {
        }
    }
}
