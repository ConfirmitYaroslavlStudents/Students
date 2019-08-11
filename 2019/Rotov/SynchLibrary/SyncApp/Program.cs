using SynchLibrary;
using System;

namespace SyncApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var master = args[0];
            var slave = args[1];
            var canRemove = args[2];
            int loggerMode = Convert.ToInt32(args[3]);
            BaseSynchronizer sync = SynchronizerFactory.Create(canRemove);
            ILogger logger = LoggerFactory.Create(loggerMode);
            FileSystemHandler handler = new FileSystemHandler(master , slave , logger);
            sync.SetUpBaseCollections(handler.MasterList , handler.SlaveList);
            var resultLogger = sync.Synchronize(handler);
            LoggerForConsole.PrintLog(resultLogger.GetLogs());
        }
    }
}
