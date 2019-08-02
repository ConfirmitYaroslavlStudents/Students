using System.IO;

namespace MasterSlaveSync
{
    public class MasterWatcher
    {
        private DirectoryInfo slave;
        private DirectoryInfo master;
        private FileSystemWatcher watcher = new FileSystemWatcher();

        public MasterWatcher(DirectoryInfo master, DirectoryInfo slave)
        {
            this.slave = slave;
            this.master = master;

            watcher.Path = master.FullName;
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
                new Conflict(new FileInfo(e.FullPath),
                WatcherHelpers.GetFileWithTheSameName(slave.FullName, e.Name)));

        }

        private void OnCreated(object source, FileSystemEventArgs e)
        {
            if (!File.Exists(Path.Combine(slave.FullName, e.Name)))
            {
                WatcherHelpers.CreateFile(slave.FullName, e.Name);
            }
            else
            {
                SyncEngine.ResolveConflict(
                     new Conflict(new FileInfo(e.FullPath),
                     WatcherHelpers.GetFileWithTheSameName(slave.FullName, e.Name)));
            }

        }

        private void OnDeleted(object source, FileSystemEventArgs e)
        {
            WatcherHelpers.DeleteFile(slave.FullName, e.Name);
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {

            if (!File.Exists(Path.Combine(slave.FullName, e.Name)))
            {
                WatcherHelpers.CreateFile(slave.FullName, e.Name);
            }
            else
            {
                SyncEngine.ResolveConflict(
                     new Conflict(new FileInfo(e.FullPath),
                     WatcherHelpers.GetFileWithTheSameName(slave.FullName, e.Name)));
            }

            WatcherHelpers.DeleteFile(slave.FullName, e.OldName);

        }
    }
}
