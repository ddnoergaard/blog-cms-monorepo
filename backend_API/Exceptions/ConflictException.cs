using Npgsql;

namespace backend_API.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string message, Exception innerException) 
            : base(message, innerException) {}
    }
}
