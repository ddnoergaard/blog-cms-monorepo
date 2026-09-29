using Npgsql;

namespace backend_API.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message, Exception? innerException = null) : base(message, innerException)
        {
            
        }
    }
}
