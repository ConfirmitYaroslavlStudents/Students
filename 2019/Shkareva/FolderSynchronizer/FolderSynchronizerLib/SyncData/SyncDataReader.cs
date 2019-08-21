// TODO : RemoveCollision
namespace FolderSynchronizerLib
{
    public class SyncDataReader
    {
        public ISyncDataReaderStrategy ReaderStrategy { get; private set; }
        
        public SyncData Load(FolderSet folderSet)
        {
            if (folderSet.NoDeleteFlag)
            {
                ReaderStrategy = new SyncDataReaderStrategy();
            }
            else
            {
                ReaderStrategy = new SyncDataReaderNoDeleteStrategy();
            }

            return ReaderStrategy.MakeSyncData(folderSet);
        }

        
    }
}
