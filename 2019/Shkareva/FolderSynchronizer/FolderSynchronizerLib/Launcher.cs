namespace FolderSynchronizerLib
{
    public class Launcher
    {
        public void Synchronize(string[] args)
        {
            var input = new InputDataReader().Read(args);
            var folderSet = new FolderSet(input);
            var syncData = SyncDataReader.Load(folderSet);
            Synchronizer.Synchronize(syncData);
            new Log().Print(syncData);
            new FolderWorker().SerializeFolder(input.MasterPath);
            new FolderWorker().SerializeFolder(input.SlavePath);

        }
    }
}
