using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            var left = (
                from x in MasterDirectory.GetContainment()
                join y in SlaveDirectory.GetContainment()
                    on new {Name = x.Name(), Type = x.GetType()} equals new {Name = y.Name(), Type = y.GetType()} into
                    temp
                from z in temp.DefaultIfEmpty()
                where x.CompareTo(z) != 0
                select new Conflict(x, z)
            ).ToList();

            var right = (
                from x in SlaveDirectory.GetContainment()
                join y in MasterDirectory.GetContainment()
                    on new {Name = x.Name(), Type = x.GetType()} equals new {Name = y.Name(), Type = y.GetType()} into
                    temp
                from z in temp.DefaultIfEmpty()
                where x.CompareTo(z) != 0
                select new Conflict(z, x)
            ).ToList();

            return left.Union(right).ToHashSet().ToList();
        }
    }
}