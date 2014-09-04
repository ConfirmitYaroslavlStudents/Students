using System;

namespace LogService
{
    public static class Logger
    {
        private static readonly ILogger LoggingImplementation;

        static Logger()
        {
            LoggingImplementation= new Log4Net();
        }

        public static void Debug(string message)
        {
            LoggingImplementation.Debug(message);
        }

        public static void Info(string message)
        {
            LoggingImplementation.Info(message); 
        }

        public static void Warn(string message)
        {
            LoggingImplementation.Warn(message);
        }

        public static void Warn(string message, Exception exception)
        {
            LoggingImplementation.Warn(message,exception);
        }

        public static void Error(string message, Exception exception)
        {
            LoggingImplementation.Error(message,exception);
        }

        public static void Fatal(string message, Exception exception)
        {
            LoggingImplementation.Fatal(message,exception);
        }
        
    }
}
