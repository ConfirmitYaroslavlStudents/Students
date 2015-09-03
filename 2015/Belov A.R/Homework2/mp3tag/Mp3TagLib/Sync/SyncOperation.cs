using System;

namespace Mp3TagLib.Sync
{
    [Serializable]
    public abstract class SyncOperation
    {
        protected bool _isCanceled;

        protected IMp3File _file;

        protected Mp3Memento _memento;

        public IMp3File File { get { return _file; } }

        public abstract bool Call(Mask mask,Tager tager, IMp3File file);

        public virtual void Cancel()
        {
            if (!_isCanceled)
            {
                RestoreFile();
                _isCanceled = true;
            }
        }

        public virtual void Redo()
        {
            if (_isCanceled)
            {
                RestoreFile();
                _isCanceled = false;
            }
        }

        protected void RestoreFile()
        {
            var newMemento = _file.GetMemento();
            _file.SetMemento(_memento);
            _memento = newMemento;

            _file.Save();
        }

        public void Save()
        {
            _file.Save();
        }
    }
}