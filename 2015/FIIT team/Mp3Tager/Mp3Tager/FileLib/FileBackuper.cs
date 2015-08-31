using System;
using System.IO;

namespace FileLib
{
    public class FileBackuper : IDisposable
    {
        private IMp3File _sourceFile;
        private IMp3File _tempFile;
        private bool _disposed;

        public FileBackuper(IMp3File sourceFile)
        {
            if (sourceFile == null)
                throw new InvalidOperationException();
            _disposed = false;
            _sourceFile = sourceFile;
            _tempFile = MakeBackup();
        }

        private IMp3File MakeBackup()
        {
            var tempDirectory = Path.GetTempPath();
            var fileName = Path.GetFileName(_sourceFile.FullName);
            return _sourceFile.CopyTo(tempDirectory + fileName);
        }

        public bool RestoreFromBackup()
        {
            if (_tempFile == null)
                return false;
            _sourceFile.Delete();
            _tempFile.MoveTo(_sourceFile.FullName);
            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _tempFile.Delete();
                _tempFile = null;
                _sourceFile = null;
            }

            _disposed = true;
        }
    }
}