using System.Collections.Generic;
using System.Linq;

namespace SynchLibrary
{

    public abstract class BaseSynchronizer
    {
        public IEnumerable<FileWrapper> Intersection { get; private set; }

        public IEnumerable<FileWrapper> MasterWithoutSlave { get; private set; }

        public IEnumerable<FileWrapper> SlaveWithoutMaster { get; private set; }

        public void SetUpBaseCollections(List<FileWrapper> master, List<FileWrapper> slave)
        {
            Intersection = master.Intersect(slave);
            MasterWithoutSlave = master.Except(slave);
            SlaveWithoutMaster = slave.Except(master);
        }
        public abstract ILogger Synchronize(FileSystemHandler handler);
    }
}
