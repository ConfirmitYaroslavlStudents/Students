using log4net.Appender;

namespace HospitalLib.Utils.LoggingTarget
{
    public abstract class LoggingTarget
    {
        internal abstract IAppender Appender { get; }
    }
}