using System;
using System.Text;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;


namespace LogService
{
    internal class Log4Net : ILogger
    {
        private readonly ILog _logger;
        public Log4Net()
        {
            _logger = LogManager.GetLogger("log.log");
            var appender = new FileAppender()
            {
                Layout = new PatternLayout("%date (%p) %message%newline"),
                File = "log.log",
                Encoding = Encoding.UTF8,
                AppendToFile = true,
                LockingModel = new FileAppender.MinimalLock()
            };
            appender.ActivateOptions();

            BasicConfigurator.Configure(appender);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Warn(string message, Exception exception)
        {
            _logger.Warn(message, exception);
        }

        public void Error(string message, Exception exception)
        {
            _logger.Error(message, exception);
        }

        public void Fatal(string message, Exception exception)
        {
            _logger.Fatal(message, exception);
        }
    }
}
