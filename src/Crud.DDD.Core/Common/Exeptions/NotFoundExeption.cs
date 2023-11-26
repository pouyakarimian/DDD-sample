namespace Crud.DDD.Core.Common.Exeptions
{
    public class NotFoundExeption : Exception
    {
        public NotFoundExeption(string message) : base(message) { }
        public NotFoundExeption(string message, Exception exception)
            : base(message, exception) { }
    }
}
