using SynchLibrary;
using System;

namespace SyncApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var master = @"C:\Users\Владислав\Desktop\master";
            var slave = @"C:\Users\Владислав\Desktop\slave";
            BaseSynchronizer sync = SynchronizerFactory.Create("false");
            ILogger logger = LoggerFactory.Create(2);
            FileSystemHandler handler = new FileSystemHandler(master , slave , logger);
            sync.SetUpBaseCollections(handler.MasterList , handler.SlaveList);
            var resultLogger = sync.Synchronize(handler);
            LoggerForConsole.PrintLog(resultLogger.GetLogs());
        }
    }
}
