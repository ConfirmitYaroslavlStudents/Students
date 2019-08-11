using System.Collections.Generic;
using System.Linq;

namespace SynchLibrary
{

    public abstract class BaseSynchronizer
    {
        public List<FileWrapper> Intersection { get; private set; }

        public List<FileWrapper> MasterWithoutSlave { get; private set; }

        public List<FileWrapper> SlaveWithoutMaster { get; private set; }

        public void SetUpBaseCollections(List<FileWrapper> master, List<FileWrapper> slave)
        {
            Intersection = master.Intersect(slave).ToList();
            MasterWithoutSlave = master.Except(slave).ToList();
            SlaveWithoutMaster = slave.Except(master).ToList();
        }
        public abstract ILogger Synchronize(FileSystemHandler handler);
    }
}
