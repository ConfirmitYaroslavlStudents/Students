using System;
using MasterSlaveSync;

namespace SyncFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter master directory path");
            string masterPath = @"C:\Users\Anna\Documents\Master"; //Console.ReadLine();
            Console.WriteLine("Enter slave directory path");
            string slavePath = @"C:\Users\Anna\Documents\Slave";//Console.ReadLine();

            Console.WriteLine("Enable no-delete: ndt, Disable no-delete: ndf");
            Console.WriteLine("Start synchronization: start");

            string input;
            Synchronizer sync = new Synchronizer(masterPath, slavePath);
            do
            {
                input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "ndt":
                        sync.SyncOptions.NoDelete = true;
                        break;
                    case "ndf":
                        sync.SyncOptions.NoDelete = false;
                        break;
                    case "start":
                        Console.WriteLine("Sync started..");
                        var res = sync.CollectConflicts();
                        sync.LogListener = Console.WriteLine;
                        sync.LogLevel = LogLevels.Verbose;
                        sync.SyncDirectories(res);
                        break;
                }
            }
            while (input != "start");

            Console.ReadKey();

        }
    }
}
