namespace Utils
{
    public class AutorizationException : Exception
    {
        public AutorizationException()
        {
        }

        public AutorizationException(string? message) : base(message)
        {
        }

        public AutorizationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}
