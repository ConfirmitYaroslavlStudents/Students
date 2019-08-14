using MasterSlaveSync.Conflict;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace MasterSlaveSync
{
    public class ConflictsRetriever
    {
        private readonly IFileSystem _fileSystem;

        public ConflictsRetriever()
            : this(new DefaultFileComparer(), new FileSystem()) { }

        public ConflictsRetriever(IEqualityComparer<IFileInfo> fileContainmentComparer)
            : this(fileContainmentComparer, new FileSystem()) { }

        public ConflictsRetriever(IFileSystem fileSystem)
            : this(new DefaultFileComparer(), fileSystem) { }

        public ConflictsRetriever(IEqualityComparer<IFileInfo> fileContainmentComparer, IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            FileContainmentComparer = fileContainmentComparer;
        }

        public IEqualityComparer<IFileInfo> FileContainmentComparer { get; private set; }

        public IEnumerable<IConflict> GetConflicts(IDirectoryInfo master, IDirectoryInfo slave) 
            => GetFileConflicts(master, slave).Concat(GetDirectoryConflicts(master, slave));

        private IEnumerable<IConflict> GetFileConflicts(IDirectoryInfo master, IDirectoryInfo slave)
        {
            var masterFiles = master.GetFiles();
            var slaveFiles = slave.GetFiles();

            var inSlaveButNotInMaster = from s in slaveFiles
                                        join m in masterFiles on s.Name equals m.Name into t
                                        from f in t.DefaultIfEmpty()
                                        where f == null
                                        select new FileConflict(null, s);
            var inMasterButNotInSlave = from m in masterFiles
                                        join s in slaveFiles on m.Name equals s.Name into t
                                        from f in t.DefaultIfEmpty()
                                        where f == null
                                        select new FileConflict(m, null);
            var inBoth = from m in masterFiles
                         join s in slaveFiles on m.Name equals s.Name 
                         where !FileContainmentComparer.Equals(m, s)
                         select new FileConflict(m, s);

            return inSlaveButNotInMaster.Concat(inMasterButNotInSlave).Concat(inBoth);

        }

        private IEnumerable<IConflict> GetDirectoryConflicts(IDirectoryInfo master, IDirectoryInfo slave)
        {
            var masterDirectories = master.GetDirectories();
            var slaveDirectories = slave.GetDirectories();

            var inSlaveButNotInMaster = from s in slaveDirectories
                                        join m in masterDirectories on s.Name equals m.Name into t
                                        from f in t.DefaultIfEmpty()
                                        where f == null
                                        select new DirectoryConflict(null, s);

            var inMasterButNotInSlave = from m in masterDirectories
                                        join s in slaveDirectories on m.Name equals s.Name into t
                                        from f in t.DefaultIfEmpty()
                                        where f == null
                                        select new DirectoryConflict(m, null);

            return inSlaveButNotInMaster.Concat(inMasterButNotInSlave);
        }
           
    }
}
