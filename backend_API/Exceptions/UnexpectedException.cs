using Npgsql;

namespace backend_API.Exceptions
{
    public class UnexpectedException : Exception
    {
        public UnexpectedException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}
