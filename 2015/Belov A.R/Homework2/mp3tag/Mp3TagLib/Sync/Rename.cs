using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mp3TagLib.Sync
{
    [Serializable]
    public class Rename:SyncOperation
    {

        public override bool Call(Mask mask,Tager tager,IMp3File file)
        {
            if (_isCanceled)
            {
                RestoreFile();
                return true;
            }
            _file = file;
            try
            {
                _memento = _file.GetMemento();
                tager.ChangeName(mask);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

     

 
    }
}
