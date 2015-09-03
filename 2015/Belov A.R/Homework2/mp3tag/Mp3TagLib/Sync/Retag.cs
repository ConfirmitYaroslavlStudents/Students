using System;

namespace Mp3TagLib.Sync
{
    [Serializable]
    public class Retag : SyncOperation
    {
        public override bool Call(Mask mask, Tager tager, IMp3File file)
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
                tager.ChangeTags(tager.GetTagsFromName(mask));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Call(Mp3Tags tags, Tager tager)
        {
            _isCanceled = false;
            _file = tager.CurrentFile;

            try
            {
                _memento = _file.GetMemento();
                tager.ChangeTags(tags);
                
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}