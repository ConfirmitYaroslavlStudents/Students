namespace FolderSynchronizerLib
{
    public class Launcher
    {
        public void Synchronize(InputData input)
        {
            var folderSet = new FolderSet(input);
            var syncData = SyncDataReader.Load(folderSet);
            var log = new Log(input.LogFlag);
            new SyncProcessor(new SyncProcessorManager()).Synchronize(syncData, input.FoldersPaths);
            log.Print(syncData);

            foreach(var path in input.FoldersPaths)
            {
                new FolderWorker().SerializeFolder(path);
            }
        }
    }
}
