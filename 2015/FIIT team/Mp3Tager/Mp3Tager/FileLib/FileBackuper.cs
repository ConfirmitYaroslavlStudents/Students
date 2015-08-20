using System;
using System.IO;

namespace FileLib
{
    // TODO: *done* IDispose pattern
    public class FileBackuper: IDisposable
    {
        private IMp3File _sourceFile;
        private bool _disposed;

        // todo: *done* make private?
        private IMp3File _tempFile;
        
        public FileBackuper(IMp3File sourceFile)
        {
            _disposed = false;
            if (sourceFile == null)
                throw new InvalidOperationException();
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