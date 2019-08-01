using System.IO;

namespace MasterSlaveSync
{
    public class SlaveWatcher
    {
        private DirectoryInfo master;
        private DirectoryInfo slave;

        public SlaveWatcher(DirectoryInfo master, DirectoryInfo slave)
        {
            this.master = master;
            this.slave = slave;
        }

        public void WatchDirectory()
        {
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = slave.FullName;

                watcher.NotifyFilter = NotifyFilters.LastWrite
                                     | NotifyFilters.FileName;

                watcher.Changed += OnChanged;
                watcher.Created += OnCreated;
                watcher.Deleted += OnDeleted;
                watcher.Renamed += OnRenamed;

                watcher.EnableRaisingEvents = true;
                while (true) ;
            }
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            SyncEngine.ResolveConflict(
                new Conflict(new FileInfo(e.FullPath),
                WatcherHelpers.GetFileWithTheSameName(master.FullName, e.Name)));
        }

        private void OnCreated(object source, FileSystemEventArgs e)
        {
            if (!Synchronizer.NoDelete)
            {
                WatcherHelpers.DeleteFile(slave.FullName, e.Name);
            }
        }

        private void OnDeleted(object source, FileSystemEventArgs e)
        {
            WatcherHelpers.CopyFile(master.FullName, slave.FullName, e.Name);
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            WatcherHelpers.CopyFile(master.FullName, slave.FullName, e.OldName);

            if (!Synchronizer.NoDelete)
            {
                WatcherHelpers.DeleteFile(slave.FullName, e.Name);
            }
        }
    }
}
