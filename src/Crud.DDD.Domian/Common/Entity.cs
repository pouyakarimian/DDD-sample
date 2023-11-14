namespace Crud.DDD.Core.Common
{
    public abstract class Entity<TKey> where TKey : notnull
    {
        private DateTime _createDate;
        protected Entity(TKey id,
            DateTime modifyDate,
            bool isDeleted)
        {
            Id = id;
            ModifyDate = modifyDate;
            IsDeleted = isDeleted;
        }

        protected Entity(TKey id,
           DateTime modifyDate)
        {
            Id = id;
            ModifyDate = modifyDate;
        }

        protected Entity(TKey id)
        {
            Id = id;
        }


        public TKey Id { get; private set; }

        public DateTime CreationDate
        {
            get
            {
                if (_createDate == default(DateTime)) _createDate = DateTime.Now;
                return _createDate;
            }
            set => _createDate = value;
        }
        public DateTime? ModifyDate { get; private set; }
        public bool IsDeleted { get; protected set; }
    }
}
