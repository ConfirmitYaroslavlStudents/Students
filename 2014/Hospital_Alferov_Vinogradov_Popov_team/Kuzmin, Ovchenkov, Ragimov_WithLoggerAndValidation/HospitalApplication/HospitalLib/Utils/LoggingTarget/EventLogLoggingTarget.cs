using log4net.Appender;

namespace HospitalLib.Utils.LoggingTarget
{
    public class EventLogLoggingTarget : LoggingTarget
    {
        internal override IAppender Appender
        {
            get { return new EventLogAppender(); }
        }
    }
}