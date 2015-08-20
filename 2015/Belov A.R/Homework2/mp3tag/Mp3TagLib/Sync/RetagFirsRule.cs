using System;
using System.Collections.Generic;

namespace Mp3TagLib.Sync
{
    public class RetagFirsRule:ISyncRule
    {
        public List<Func<Mask, Tager, bool>> OperationsList { get; private set; }

        public RetagFirsRule()
        {
            var operations=new SyncMethods();
            OperationsList = new List<Func<Mask, Tager, bool>> {operations.TryChangeTags, operations.TryChangeName};
        }
    }
}
