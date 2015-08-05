using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mp3Tager
{
    public class BackupMp3Tager
    {
        private string _sourcePath;
        private string _destinationPath; 
        private Backuper.Backuper _backup;

        public void MakeMp3Backup(string[] args)
        {
            var path = FindPath(args);            
            if (path !=null)
            {
                _sourcePath = GetPathToFolder(path);
                _destinationPath = ConfigurationManager.AppSettings["destination"];

                _backup = new Backuper.Backuper(_sourcePath, _destinationPath);
                _backup.MakeBackup();
            }
        }

        public void RestoreFromMp3Backup()
        {
            _backup.RestoreFromBackUp();
        }
      
        private string FindPath(string[] args)
        {
            Regex regex = new Regex(@"[A-Z]:\\.*\.mp3");

            return args.FirstOrDefault(str => regex.IsMatch(str));
        }

        private string GetPathToFolder(string fullPath)
        {
            return Path.GetDirectoryName(fullPath);
        }
    }
}
