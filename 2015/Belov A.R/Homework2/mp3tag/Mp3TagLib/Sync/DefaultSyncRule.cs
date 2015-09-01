using System;
using System.Collections.Generic;

namespace Mp3TagLib.Sync
{
    [Serializable]
    public class DefaultSyncRule:ISyncRule
    {

        public List<SyncOperation> OperationsList { get; private set; }

        public DefaultSyncRule()
        {
            OperationsList = new List<SyncOperation> {new Rename(), new Retag() };

        }

        public ISyncRule Clone()
        {
            return new DefaultSyncRule();
        }
    }
}
