using System.IO;

namespace MasterSlaveSync
{
    public class Synchronizer
    {
        public static bool NoDelete { get; set; } = false;

        private DirectoryInfo master;
        private DirectoryInfo slave;

        public Synchronizer(DirectoryInfo master, DirectoryInfo slave)
        {
            this.master = master;
            this.slave = slave;
        }

        public void Run()
        {
            SyncEngine.SyncNoConflict(master, slave);
            SyncEngine.SyncConflict(master, slave);

            var masterWatcher = new MasterWatcher(master, slave);
            var slaveWatcher = new SlaveWatcher(master, slave);

            masterWatcher.WatchDirectory();
            slaveWatcher.WatchDirectory();
        }

    }
}
