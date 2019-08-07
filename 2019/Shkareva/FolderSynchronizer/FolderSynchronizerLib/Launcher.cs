namespace FolderSynchronizerLib
{
    public class Launcher
    {
        public void Synchronize(string[] args)
        {
            var syncData = SyncDataReader.Load(args);
            Synchronizer.Synchronize(syncData);
            new Log().Print(syncData);
            new FolderWorker().SerializeFolder(args[0]);
            new FolderWorker().SerializeFolder(args[1]);

        }
    }
}
