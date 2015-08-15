using System;
using System.IO;

namespace FileLib
{
    // TODO: Immutable file model
    public class FileBackuper: IDisposable
    {
        private IMp3File _sourceFile;
        private IMp3File _tempFile;
        private bool _restored;

        public IMp3File MakeBackup(IMp3File sourceFile)
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
            _tempFile.MoveTo(new FileExistenceChecker(), _sourceFile.FullName);
            _restored = true;
        }

        public void Dispose()
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
