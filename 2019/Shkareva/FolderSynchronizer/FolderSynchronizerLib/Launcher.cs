namespace FolderSynchronizerLib
{
    public class Launcher
    {
        public void Synchronize(InputData input)
        {
            var folderSet = new FolderSet(input);
            var syncData = SyncDataReader.Load(folderSet);
            new SyncProcessor(new SyncProcManager()).Synchronize(syncData);
            new Log().Print(syncData);
            new FolderWorker().SerializeFolder(input.MasterPath);
            new FolderWorker().SerializeFolder(input.SlavePath);

        }
    }
}
