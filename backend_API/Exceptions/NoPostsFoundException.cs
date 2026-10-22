namespace backend_API.Exceptions
{
    public class NoPostsFoundException : Exception
    {
        public NoPostsFoundException(string message, Exception? innerException = null) : base(message, innerException)
        {
            
        }
    }
}
