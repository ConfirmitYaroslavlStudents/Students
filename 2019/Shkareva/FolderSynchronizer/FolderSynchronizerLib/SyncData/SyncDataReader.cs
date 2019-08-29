// TODO : RemoveCollision
using System;

namespace FolderSynchronizerLib
{
    public class SyncDataReader
    {
        public ISyncDataReaderStrategy SyncReaderStrategy { get; set; }

        
        public SyncData Load(FolderSet folderSet)
        {
            if (folderSet.NoDeleteFlag)
            {
                SyncReaderStrategy = new SyncDataReaderDeleteStrategy();
            }
            else
            {
                SyncReaderStrategy = new SyncDataReaderNoDeleteStrategy();
            }

            var syncData = SyncReaderStrategy.MakeSyncData(folderSet);

            return syncData;
        }

        
    }
}
