namespace backend_API.Exceptions
{
    public class NoPostFoundException : Exception
    {
        public NoPostFoundException(string message, Exception innerException = null) : base(message, innerException)
        {
            
        }
    }
}
