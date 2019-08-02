using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sync
{
    public class ConflictsCollector
    {
        public DirectoryInfo MasterDirectory { get; }
        public DirectoryInfo SlaveDirectory { get; }

        private readonly ConflictSeeker _conflictSeeker;

        public ConflictsCollector(DirectoryInfo master, DirectoryInfo slave, IConflictDetectionPolicy policy)
        {
            MasterDirectory = master;
            SlaveDirectory = slave;
            _conflictSeeker = new ConflictSeeker(policy);
        }

        public List<Conflict> GetConflicts()
        {
            return DFS(MasterDirectory, SlaveDirectory);
        }

        private List<Conflict> DFS(DirectoryInfo master, DirectoryInfo slave)
        {
            var children =
                from x in master.EnumerateDirectories()
                join y in slave.EnumerateDirectories() on x.Name equals y.Name
                select new {Master = x, Slave = y};

            var conflicts = _conflictSeeker.GetConflicts(master, slave);

            foreach (var child in children)
                conflicts.AddRange(DFS(child.Master, child.Slave));

            return conflicts;
        }
    }
}