using System;
using System.IO;

namespace Backuper
{
    public class Backuper
    {
        private string _source; 
        private string _destination;

        public Backuper(string source, string destination)
        {
            _source = source;
            _destination = destination;
        }

        private void CreateDirectories()
        {
            foreach (string dirPath in Directory.GetDirectories(_source, "*", SearchOption.AllDirectories))
            {
                string newDir = dirPath.Replace(_source, _destination);
                if (!Directory.Exists(newDir))
                {
                    Directory.CreateDirectory(newDir);
                }
            }
        }

        private void CopyFiles()
        {
            foreach (string sourcePath in Directory.GetFiles(_source, "*", SearchOption.AllDirectories))
            {
                    var destPath = sourcePath.Replace(_source, _destination);
                    File.Copy(sourcePath, destPath, true);                            
            }
        }

        public void MakeBackup()
        {
            try
            {
                CreateDirectories();
                CopyFiles();
            }
            catch (IOException e)
            {
                throw new Exception("A copy error occured.");
            }
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
