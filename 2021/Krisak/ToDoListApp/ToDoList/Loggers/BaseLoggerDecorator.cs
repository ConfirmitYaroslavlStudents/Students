namespace ToDoLibrary
{
    public class BaseLoggerDecorator : Loggers.ILogger
    {
        private Loggers.ILogger _logger;

        public BaseLoggerDecorator(Loggers.ILogger logger)
        {
            _logger = logger;
        }
        public virtual void Log(string message)
        {
            _logger.Log(message);
        }
    }
}
