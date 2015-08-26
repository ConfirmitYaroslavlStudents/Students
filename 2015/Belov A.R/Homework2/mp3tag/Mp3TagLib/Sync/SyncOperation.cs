using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mp3TagLib.Sync
{
    public abstract class SyncOperation
    {
        public abstract bool Call(Mask mask, Tager tager);
    }
}
