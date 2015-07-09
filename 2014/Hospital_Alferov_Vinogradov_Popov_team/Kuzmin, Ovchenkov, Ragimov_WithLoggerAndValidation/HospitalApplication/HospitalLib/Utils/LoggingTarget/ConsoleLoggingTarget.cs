using log4net.Appender;

namespace HospitalLib.Utils.LoggingTarget
{
    public class ConsoleLoggingTarget : LoggingTarget
    {
        internal override IAppender Appender
        {
            get { return new ConsoleAppender(); }
        }
    }
}