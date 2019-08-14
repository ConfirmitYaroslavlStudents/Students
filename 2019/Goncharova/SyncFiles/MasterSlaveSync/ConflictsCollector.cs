using MasterSlaveSync.Conflict;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace MasterSlaveSync
{
    public class ConflictsCollector
    {
        private IFileSystem _fileSystem;
        private ConflictsRetriever conflictsRetriever;

        public ConflictsCollector(ConflictsRetriever conflictsRetriever, IFileSystem fileSystem)
        {
            this.conflictsRetriever = conflictsRetriever;
            _fileSystem = fileSystem;
        }

        public ConflictsCollector(ConflictsRetriever conflictsRetriever)
            : this(conflictsRetriever, new FileSystem()) { }

        public ConflictsCollector(IFileSystem fileSystem)
            : this(new ConflictsRetriever(), fileSystem) { }

        public ConflictsCollector()
            : this(new ConflictsRetriever(), new FileSystem()) { }


        public List<IConflict> CollectConflicts(IDirectoryInfo master, IDirectoryInfo slave)
        {
            var inBoth = from m in master.EnumerateDirectories()
                         join s in slave.EnumerateDirectories() 
                         on m.Name equals s.Name
                         select new {Master = m, Slave = s };

            var result = conflictsRetriever.GetConflicts(master, slave);

            foreach (var directoriesPair in inBoth)
            {
                result = result.Concat(CollectConflicts(directoriesPair.Master, directoriesPair.Slave));
            }

            return result.ToList();

        }

    }
}
