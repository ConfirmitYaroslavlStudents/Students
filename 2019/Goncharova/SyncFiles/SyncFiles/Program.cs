using System;

namespace Sync
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintUsage();

            var arguments = new SyncConfigurator(args);
            var synchronizer = arguments.GetSynchronizer();

            if (arguments.ErrorMessage == String.Empty)
            {
                Console.WriteLine("Started synchronization...");
                synchronizer.Run();
            }
            else
            {
                Console.WriteLine(arguments.ErrorMessage);
                PrintUsage();
            }

            Console.ReadKey();
        }

        private static void PrintUsage()
        {
            Console.WriteLine(@"Usage: Sync.exe <source> <destination> [-nodelete] [-logsummary] [-logverbose]");
        }
    }
}
