namespace Crud.DDD.Core.Common
{
    public interface IFullAudited
    {
        public Guid CreateUserId { get; set; }
        public Guid? ModifyUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? ModifyTime { get; set; }
    }
}
