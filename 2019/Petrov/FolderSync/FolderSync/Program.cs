using System;
using System.IO;

namespace FolderSync
{
    class Program
    {
        static void Main(string[] args)
        {
            var masterPath = Console.ReadLine();
            var slavePath = Console.ReadLine();
            var flag = Console.ReadLine();
            var masterDirectory = new DirectoryInfo(masterPath);
            var slaveDirectory = new DirectoryInfo(slavePath);
            bool noDelete;
            if (flag == "no delete")
                noDelete = true;
            else
                noDelete = false;
            var sync = new Synch(masterDirectory, slaveDirectory, noDelete);
            sync.Synchronization();
            sync.log.PrintLog("summary");
        }
    }
}
/*
C:\dir1
C:\dir2
delete
 */
