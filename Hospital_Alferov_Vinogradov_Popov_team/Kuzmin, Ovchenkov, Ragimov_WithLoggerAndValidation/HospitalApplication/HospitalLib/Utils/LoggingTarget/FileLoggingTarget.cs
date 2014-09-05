using System.IO;
using System.Reflection;
using log4net.Appender;

namespace HospitalLib.Utils.LoggingTarget
{
    public class FileLoggingTarget : LoggingTarget
    {
        private static readonly string AssemblyPath =
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase.Substring(8));

        private readonly string _logFilePath;

        public FileLoggingTarget()
        {
            const string logFileName = "log.txt";
            _logFilePath = Path.Combine(AssemblyPath, logFileName);
        }

        public FileLoggingTarget(string pathToLogFile)
        {
            _logFilePath = pathToLogFile;
        }

        internal override IAppender Appender
        {
            get { return new FileAppender {File = _logFilePath, AppendToFile = false}; }
        }
    }
}