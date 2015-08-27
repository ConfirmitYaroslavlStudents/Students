using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mp3TagLib.Sync
{
    [Serializable]
    class Rename:SyncOperation
    {
        public override bool Call(Mask mask, Tager tager)
        {
            try
            {
                tager.ChangeName(mask);
                return true;
            }
            catch (Exception)
            {

                return false;
            };
        }
    }
}
