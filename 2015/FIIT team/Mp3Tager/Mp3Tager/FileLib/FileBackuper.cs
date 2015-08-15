using System;
using System.IO;

namespace FileLib
{
    // TODO: Immutable file model
    public class FileBackuper: IDisposable
    {
        private IMp3File _sourceFile;

        public IMp3File TempFile { get; private set; }
        
        public FileBackuper(IMp3File sourceFile)
        {
            if (sourceFile == null)
                throw new InvalidOperationException();
            _sourceFile = sourceFile;
            TempFile = MakeBackup();
        }

        private IMp3File MakeBackup()
        {
            var tempDirectory = Path.GetTempPath();
            var fileName = Path.GetFileName(_sourceFile.FullName);
            return _sourceFile.CopyTo(tempDirectory + fileName);
        }

        public void RestoreFromBackup()
        {            
            _sourceFile.Delete();
            TempFile.MoveTo(new FileExistenceChecker(), _sourceFile.FullName);
        }

        public void Dispose()
        {
            TempFile.Delete();
            TempFile = null;            
            _sourceFile = null;
        }
    }
}
