using MasterSlaveSync.Conflicts;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace MasterSlaveSync
{
    public class ConflictsRetriever
    {
        public ConflictsRetriever()
            : this(new DefaultFileComparer()) { }
        public ConflictsRetriever(IEqualityComparer<IFileInfo> fileContentComparer)
        {
            FileContentComparer = fileContentComparer;
        }
        public IEqualityComparer<IFileInfo> FileContentComparer { get; private set; }

        public ConflictsCollection GetConflicts(IDirectoryInfo master, IDirectoryInfo slave)
        {
            var result = new ConflictsCollection();
            result.fileConflicts.AddRange(GetFileConflicts(master, slave));
            result.directoryConflicts.AddRange(GetDirectoryConflicts(master, slave));

            return result;
        }

        private IEnumerable<FileConflict> GetFileConflicts(IDirectoryInfo master, IDirectoryInfo slave)
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
            var inBothMasterAndSlave = from m in masterFiles
                         join s in slaveFiles on m.Name equals s.Name 
                         where !FileContentComparer.Equals(m, s)
                         select new FileConflict(m, s);

            return inSlaveButNotInMaster.Concat(inMasterButNotInSlave).Concat(inBothMasterAndSlave);

        }

        private IEnumerable<DirectoryConflict> GetDirectoryConflicts(IDirectoryInfo master, IDirectoryInfo slave)
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
