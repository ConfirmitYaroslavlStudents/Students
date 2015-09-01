using System;
using System.Collections.Generic;

namespace Mp3TagLib.Sync
{
    public interface ISyncRule
    {
        List<SyncOperation> OperationsList { get; }

        ISyncRule Clone();
    }
}
