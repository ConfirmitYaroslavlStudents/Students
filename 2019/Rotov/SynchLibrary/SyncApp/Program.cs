using SynchLibrary;

namespace SyncApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var master = @".\master";
            var slave = @".\slave";
            ISynchronizer sync = SynchronizerFactory.Create("true");
            ILogger logger = LoggerFactory.Create(2);
            var result = sync.Synchronize(master , slave , logger);
            LoggerForConsole.PrintLog(result.GetLogs());
        }
    }
}
