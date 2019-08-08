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
            Console.WriteLine("Start watching: start, Stop watching: stop");

            string input;
            do
            {
                input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "ndt":
                        Synchronizer.NoDelete = true;
                        break;
                    case "ndf":
                        Synchronizer.NoDelete = false;
                        break;
                    case "start":
                        Console.WriteLine("Sync started..");
                        Synchronizer sync = new Synchronizer(masterPath, slavePath);
                        var res = sync.CollectConflicts();
                        break;
                }
            }
            while (input != "start");

            bool stop = false;

            do
            {
                switch (Console.ReadLine().ToLower())
                {
                    case "ndt":
                        Synchronizer.NoDelete = true;
                        break;
                    case "ndf":
                        Synchronizer.NoDelete = false;
                        break;
                    case "stop":
                        stop = true;
                        break;
                }

            }
            while (!stop);

        }
    }
}
