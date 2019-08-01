using System;
using System.IO;
using MasterSlaveSync;

namespace SyncFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo master = new DirectoryInfo(@"C:\Users\Anna\Documents\Master");
            DirectoryInfo slave = new DirectoryInfo(@"C:\Users\Anna\Documents\Slave");

            Synchronizer synchronizer = new Synchronizer(master, slave);
            Synchronizer.NoDelete = true;

            synchronizer.Run();

            Console.ReadKey();
        }

    }
}
