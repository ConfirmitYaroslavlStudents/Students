using System;
using System.IO;
namespace DirectorySynchronizationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var flag = Enums.Flags.Nothing;
            var masterDirectory = new DirectoryInfo(args[0]);
            var slaveDirectory = new DirectoryInfo(args[1]);
            if (args.Length == 3)
                flag = Enums.Flags.NoDelete;
            var sync = new Synch(masterDirectory, slaveDirectory, flag);
            sync.Synchronization();
            sync.log.PrintLog(Enums.LogVariants.Summary,Enums.WhereToPrint.Console);
        }
    }
}
/*
C:\dir1
C:\dir2
delete
 */
