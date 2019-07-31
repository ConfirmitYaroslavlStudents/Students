using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SyncTool
{
    public class ConflictsCollector
    {
        public DirectoryInfo MasterDirectory { get; }
        public DirectoryInfo SlaveDirectory { get; }

        public ConflictsCollector(DirectoryInfo master, DirectoryInfo slave)
        {
            MasterDirectory = master;
            SlaveDirectory = slave;
        }

        public List<Conflict> GetConflicts()
        {
            return DFS(MasterDirectory, SlaveDirectory);
        }

        private List<Conflict> DFS(DirectoryInfo master, DirectoryInfo slave)
        {
            var children = (
                from x in master.EnumerateDirectories()
                join y in slave.EnumerateDirectories() on x.Name equals y.Name
                select new {Master = x, Slave = y}
                );

            var seeker = new ConflictSeeker(master, slave);
            var conflicts = seeker.GetConflicts();

            foreach (var child in children)
                conflicts.AddRange(DFS(child.Master, child.Slave));

            return conflicts;
        }
    }
}