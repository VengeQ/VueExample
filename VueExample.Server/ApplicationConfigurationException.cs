namespace QuizesApp.Server
{
    /// <summary>
    /// Исключение, генерируемое при некорректном конфиге приложения
    /// </summary>
    public class ApplicationConfigurationException : ApplicationException
    {
        public ApplicationConfigurationException()
        {
        }

        public ApplicationConfigurationException(string? message) : base(message)
        {
        }

        public ApplicationConfigurationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
