using Sync.Resolutions;

namespace Sync.Loggers
{
    public interface ILogger
    {
        void Log(IResolution resolution);
    }
}