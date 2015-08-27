using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mp3TagLib.Sync
{
    [Serializable]
    class Retag:SyncOperation
    {
        public override bool Call(Mask mask, Tager tager)
        {
            try
            {
                tager.ChangeTags(tager.GetTagsFromName(mask));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
