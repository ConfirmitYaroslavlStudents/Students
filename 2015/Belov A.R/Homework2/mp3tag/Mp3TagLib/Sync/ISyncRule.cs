using System;
using System.Collections.Generic;

namespace Mp3TagLib.Sync
{
    public interface ISyncRule
    {
        List<Func<Mask, Tager, bool>> OperationsList { get;}
    }
}
