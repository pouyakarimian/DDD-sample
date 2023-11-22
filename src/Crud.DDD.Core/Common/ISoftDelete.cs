namespace Crud.DDD.Core.Common
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeleteTime { get; set; }
        public Guid? DeleteUserId { get; set; }
    }
}
