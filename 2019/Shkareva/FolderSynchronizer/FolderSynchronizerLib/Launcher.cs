namespace FolderSynchronizerLib
{
    public class Launcher
    {
        public void Synchronize(InputData input)
        {
            var folderSet = new FolderSet(input);
            var syncData = new SyncDataReader().Load(folderSet);
            new SyncProcessor(new SyncProcessorManager()).Synchronize(syncData, input.FoldersPaths);

            foreach(var path in input.FoldersPaths)
            {
                new FolderWorker().SerializeFolder(path);
            }
        }
    }
}
