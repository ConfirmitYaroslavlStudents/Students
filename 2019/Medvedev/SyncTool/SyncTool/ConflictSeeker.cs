using System.Collections.Generic;
using System.IO;
using System.Linq;
using SyncTool.Wrappers;

namespace SyncTool
{
    public class ConflictSeeker
    {
        public DirectoryInfo MasterDirectory { get; }
        public DirectoryInfo SlaveDirectory { get; }

        public ConflictSeeker(DirectoryInfo masterDirectory, DirectoryInfo slaveDirectory)
        {
            MasterDirectory = masterDirectory;
            SlaveDirectory = slaveDirectory;
        }

        public List<Conflict> GetConflicts()
        {
            var leftJoin = (
                from x in MasterDirectory.EnumerateFiles()
                join y in SlaveDirectory.EnumerateFiles() on x.Name equals y.Name into temp
                from z in temp.DefaultIfEmpty()
                select new Conflict(new FileInfoWrapper(x), z == null ? null : new FileInfoWrapper(z))).ToList();

            var rightJoin = (
                from x in SlaveDirectory.EnumerateFiles()
                join y in MasterDirectory.EnumerateFiles() on x.Name equals y.Name into temp
                from z in temp.DefaultIfEmpty()
                select new Conflict(z == null ? null : new FileInfoWrapper(z), new FileInfoWrapper(x))).ToList();


            return leftJoin.Union(rightJoin).ToList();
        }
    }
}