using Sync;
using Sync.Loggers;

namespace SyncTool
{
    public class Parameters
    {
        public string PathToMaster { get; set; } = null;
        public string PathToSlave { get; set; } = null;
        public ResolverOptions ResolverOption { get; set; } = ResolverOptions.None;
        public LoggerOption LoggerOption { get; set; } = LoggerOption.Silent;
    }
}