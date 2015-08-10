using System;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Tests")]
namespace FolderBackuperLib
{
    public class FolderBackuper
    {
        private ISource _source;
        internal ISource _destination;

        public FolderBackuper(ISource source, ISource destination)
        {
            _source = source;
            _destination = destination;
          
        }

        private void CreateDirectories()
        {
            foreach (string dirPath in _source.GetDirectories())
            {
                string newDir = dirPath.Replace(_source.SourceFolder, _destination.SourceFolder);
                _destination.DirectoryNames.Add(newDir);
                if (!_source.Exists(newDir))
                {
                    _source.CreateDirectory(newDir);
                }
            }       
        }

        private void CopyFiles()
        {

            foreach (string sourcePath in _source.GetFiles())
            {

                try
                {
                    var destPath = sourcePath.Replace(_source.SourceFolder, _destination.SourceFolder);
                    _destination.FileNames.Add(destPath);
                    _destination.Copy(sourcePath, destPath, true);
                }
                catch (IOException e)
                {
                    throw new Exception("A copy error occured.");
                }
            }
        }

        public void MakeBackup()
        {       
            CreateDirectories();
            CopyFiles();
        }

        public void RestoreFromBackUp()
        {
            var temp = _source;
            _source = _destination;
            _destination = temp;

            MakeBackup();
        }
    }
}
