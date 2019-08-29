namespace FolderSynchronizerLib
{
    public class Launcher
    {
        public void Synchronize(InputData input)
        {
            var folderSet = new FolderSet(input);
            
            var syncData = new SyncDataReader().Load(folderSet);
            var log = new LogCreator().Create(folderSet.Loglevel);
            new SyncProcessor().Synchronize(syncData, input.FoldersPaths, log);

            foreach(var path in input.FoldersPaths)
            {
                new FolderWorker().SerializeFolder(path);
            }
        }
    }
}
