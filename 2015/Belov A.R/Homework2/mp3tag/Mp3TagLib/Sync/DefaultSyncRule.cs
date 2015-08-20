using System;
using System.Collections.Generic;

namespace Mp3TagLib.Sync
{
    public class DefaultSyncRule:ISyncRule
    {

        public List<Func<Mask, Tager, bool>> OperationsList { get; private set; }

        public DefaultSyncRule()
        {
            var operations=new SyncMethods();
            OperationsList = new List<Func<Mask, Tager, bool>> {operations.TryChangeName, operations.TryChangeTags};

        }
    }
}
