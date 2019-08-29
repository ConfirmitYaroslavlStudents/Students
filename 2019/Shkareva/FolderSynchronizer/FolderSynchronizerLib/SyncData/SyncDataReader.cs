// TODO : RemoveCollision
using System;

namespace FolderSynchronizerLib
{
    public class SyncDataReader
    {
        public ISyncDataReaderStrategy SyncReaderStrategy { get; private set; }
        
        public SyncData Load(FolderSet folderSet)
        {
            if (folderSet.NoDeleteFlag)
            {
                SyncReaderStrategy = new SyncDataReaderStrategy();
            }
            else
            {
                SyncReaderStrategy = new SyncDataReaderNoDeleteStrategy();
            }

            var syncData = SyncReaderStrategy.MakeSyncData(folderSet);
            syncData.Log = ChooseLog(folderSet.Loglevel);

            return syncData;
        }

        private ILog ChooseLog(LogLevels loglevel)
        {
            if (loglevel == LogLevels.summary)
            {
                return new SummaryLog();
            }

            if (loglevel == LogLevels.verbose)
            {
                return new VerboseLog();
            }

            if (loglevel == LogLevels.silent)
            {
                return new SilentLog();
            }

            return null;
        }
    }
}
