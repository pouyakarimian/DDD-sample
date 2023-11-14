namespace Crud.DDD.Core.Common.Exeptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message, Exception exception)
            : base(message, exception) { }

        public BusinessException(string message)
           : base(message) { }
    }
}
