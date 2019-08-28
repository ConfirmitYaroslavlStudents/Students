using System.IO;

namespace SyncLib.Loggers
{
    public class LoggerFactory
    {
        public static ILogger GetLogger(LoggerType logType, TextWriter writer)
        {
            switch (logType)
            {
                case LoggerType.Silent:
                    return new SilentLoggerVisitor();
                case LoggerType.Verbose:
                    return new VerboseLoggerVisitor(writer);
                default:
                    return new SummaryLoggerVisitor(writer);
            }
        }
    }
}
