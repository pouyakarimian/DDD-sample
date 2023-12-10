using Crud.DDD.Core.Aggregates.OrderAggregate.ValueObjects;
using Crud.DDD.Core.Common;

namespace Crud.DDD.Core.Aggregates.Order
{
    public class Order : AggregateRoot, IFullAudited, ISoftDelete
    {
        public Order(Guid id) : base(id)
        {
        }

        #region Auditing Fields
        public Guid CreateUserId { get; set; }
        public Guid? ModifyUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? ModifyTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteTime { get; set; }
        public Guid? DeleteUserId { get; set; }
        #endregion

        public string Description { get; private set; }
        public decimal TotlaPrice { get; private set; }
        public Money Money { get; private set; }
    }
}
