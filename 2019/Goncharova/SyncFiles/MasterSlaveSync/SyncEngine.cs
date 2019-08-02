using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MasterSlaveSync
{
    internal static class SyncEngine
    {
        internal static void SyncNoConflict(DirectoryInfo master, DirectoryInfo slave)
        {
            var masterFiles = master.GetFiles();
            var slaveFiles = slave.GetFiles();

            var copyToSlave = masterFiles.Except(slaveFiles, new FileComparerByName());
            foreach (var srcFile in copyToSlave)
            {
                File.Copy(srcFile.FullName, Path.Combine(slave.FullName, srcFile.Name));
            }

            if (!Synchronizer.NoDelete)
            {
                var removefromSlave = slaveFiles.Except(masterFiles, new FileComparerByName());
                foreach (var file in removefromSlave)
                {
                    File.Delete(file.FullName);
                }
            }

        }

        internal static void SyncConflict(DirectoryInfo master, DirectoryInfo slave)
        {
            var conflicts = GetConflictPairs(master, slave);

            foreach (var conflict in conflicts)
            {
                ResolveConflict(conflict);
            }
        }

        internal static IEnumerable<Conflict> GetConflictPairs(DirectoryInfo master, DirectoryInfo slave)
        {
            return from mFile in master.EnumerateFiles()
                   join sFile in slave.EnumerateFiles() on mFile.Name equals sFile.Name
                   select (new Conflict(mFile, sFile));
        }

        internal static void ResolveConflict(Conflict conflict)
        {
            File.Copy(conflict.MasterFile.FullName, conflict.SlaveFile.FullName, true);
        }
        
    }
}
