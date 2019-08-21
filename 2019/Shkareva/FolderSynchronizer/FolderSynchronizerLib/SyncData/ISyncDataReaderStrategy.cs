namespace FolderSynchronizerLib
{
    public interface ISyncDataReaderStrategy
    {
        SyncData MakeSyncData(FolderSet folderSet);
    }
}
