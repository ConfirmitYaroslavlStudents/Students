using GeneralizeSynchLibrary;

namespace GeneralizeSyncApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var parsedArgs = new ArgParser(args);
            var synch = SynchronizerFactory.Create(parsedArgs.NoDelete);
            var logger = LoggerFactory.Create(parsedArgs.Mode);
            var collection = new FileWrapperCollection(parsedArgs.FileNames);
            var report = synch.Synchronize(collection);
            var logResult = report.ApplyResult(logger);
            LoggerForConsole.PrintLog(logResult.GetLogs());
        }
    }
}
