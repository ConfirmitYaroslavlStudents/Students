using System.Collections.Generic;
using System.Linq;
using Sync.Wrappers;

namespace Sync
{
    public class ConflictsCollector
    {
        public DirectoryWrapper MasterDirectory { get; }
        public DirectoryWrapper SlaveDirectory { get; }

        private readonly ConflictSeeker _conflictSeeker;

        public ConflictsCollector(DirectoryWrapper master, DirectoryWrapper slave, IConflictDetectionPolicy policy)
        {
            MasterDirectory = master;
            SlaveDirectory = slave;
            _conflictSeeker = new ConflictSeeker(policy);
        }

        public List<Conflict> GetConflicts()
        {
            return DFS(MasterDirectory, SlaveDirectory);
        }

        private List<Conflict> DFS(DirectoryWrapper master, DirectoryWrapper slave)
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