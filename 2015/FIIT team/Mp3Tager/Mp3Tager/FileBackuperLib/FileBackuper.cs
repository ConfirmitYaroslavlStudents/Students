using System;
using System.IO;

namespace FileBackuperLib
{
    public class FileBackuper
    {
        private IFile _sourceFile;
        private IFile _tempFile;
        private bool _restored;

        public IFile MakeBackup(IFile sourceFile)
        {
            if (_sourceFile != null)
                throw new InvalidOperationException();

            _sourceFile = sourceFile;
            _restored = false;

            var tempDirectory = Path.GetTempPath();
            var fileName = Path.GetFileName(_sourceFile.FullName);

            return _tempFile = _sourceFile.CopyTo(tempDirectory + fileName);
        }

        public void RestoreFromBackup()
        {
            if (_sourceFile == null || _tempFile == null)
                throw new InvalidOperationException();

            _sourceFile.Delete();
            _tempFile.MoveTo(_sourceFile.FullName);
            _restored = true;
        }

        public void Free()
        {
            if (_tempFile != null && _restored == false)
            {
                _tempFile.Delete();
                _tempFile = null;
            }
            _sourceFile = null;
        }
    }
}
