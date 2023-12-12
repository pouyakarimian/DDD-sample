namespace Crud.DDD.Core.Common.Exeptions
{
    public class NotFoundExeption : Exception
    {
        public NotFoundExeption(string message) : base(string.Concat(message, " Could not be find")) { }
        public NotFoundExeption(string message, Exception exception)
            : base(string.Concat(message, " Could not be find"), exception) { }
    }
}
