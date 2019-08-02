using System.IO;

namespace MasterSlaveSync
{
    public class SlaveWatcher
    {
        private DirectoryInfo master;
        private DirectoryInfo slave;
        private FileSystemWatcher watcher = new FileSystemWatcher();

        public SlaveWatcher(DirectoryInfo master, DirectoryInfo slave)
        {
            this.master = master;
            this.slave = slave;

            watcher.Path = slave.FullName;
        }

        public void WatchDirectory()
        {
            watcher.NotifyFilter = NotifyFilters.LastWrite
                                     | NotifyFilters.FileName;

            watcher.Changed += OnChanged;
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;

            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            SyncEngine.ResolveConflict(
                new Conflict(WatcherHelpers.GetFileWithTheSameName(master.FullName, e.Name), 
                new FileInfo(e.FullPath)));
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
            if(File.Exists(Path.Combine(master.FullName, e.Name)))
            {
                WatcherHelpers.CopyFile(master.FullName, slave.FullName, e.Name);
            }

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
