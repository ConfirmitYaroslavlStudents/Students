using Sync.Resolutions;

namespace Sync.Loggers
{
    public abstract class Logger
    {
        public LoggerOption Option { get; }

        protected Logger(LoggerOption option)
        {
            Option = option;
        }

        public abstract void Log(IResolution resolution);

        protected string UpdateLog(UpdateResolution resolution)
        {
            if (Option == LoggerOption.Summary)
                return $"Update {resolution.Destination.FullName}";
            if (Option == LoggerOption.Verbose)
                return $"Update {resolution.Destination.FullName} with {resolution.Source.FullName}";
            return "";
        }

        protected string CopyLog(CopyResolution resolution)
        {
            if (Option == LoggerOption.Summary)
                return $"Copy {resolution.Source.FullName}";
            if (Option == LoggerOption.Verbose)
                return $"Copy {resolution.Source.FullName} to {resolution.Destination}";
            return "";
        }

        protected string DeleteLog(DeleteResolution resolution)
        {
            if (Option == LoggerOption.Summary || Option == LoggerOption.Verbose)
                return $"Delete {resolution.Source}";
            return "";
        }
    }
}