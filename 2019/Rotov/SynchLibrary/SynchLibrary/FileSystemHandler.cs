using System.Collections.Generic;
using System.IO;

namespace SynchLibrary
{
    public class FileSystemHandler
    {
        string MasterPath { get; set; }
        string SlavePath { get; set; }
        public ILogger _logger { get; private set; }

        public FileSystemHandler(string master , string slave , ILogger logger)
        {
            MasterPath = master;
            SlavePath = slave;
            _logger = logger;
        }

        public List<FileWrapper> GetListOfFiles(string path)
        {
            var result = new List<FileWrapper>();
            foreach(var file in Directory.GetFiles(path , "*" , SearchOption.AllDirectories))
                result.Add(new FileWrapper(new FileInfo(file) , path));
            return result;
        }

        public void MoveIntersectionFiles(IEnumerable<FileWrapper> files)
        {
            foreach(var file in files)
            {
                var old = Path.Combine(MasterPath , file.RelativePath);
                var fresh = Path.Combine(SlavePath , string.Join(@"\" , file.RelativePath));
                if(FilesChanged(old , fresh))
                {
                    File.Copy(old , fresh , true);
                }
                _logger.AddReplace(file.RelativePath , SlavePath);
            }
        }

        private bool FilesChanged(string file1 , string file2)
        {
            FileInfo first = new FileInfo(file1);
            FileInfo second = new FileInfo(file2);
            if(first.Length != second.Length)
                return true;
            else
            {
                var firstSize = File.ReadAllBytes(first.FullName);
                var secondSize = File.ReadAllBytes(second.FullName);
                for(int i = 0; i < firstSize.Length; i++)
                {
                    if(firstSize[i] != secondSize[i])
                        return true;
                }
                return false;
            }
        }

        public void RemoveDisapperedFiles(IEnumerable<FileWrapper> files)
        {
            foreach(var file in files)
            {
                File.Delete(Path.Combine(SlavePath , file.RelativePath));
                _logger.AddRemove(file.RelativePath , SlavePath);
            }
        }

        public void SwapFiles(IEnumerable<FileWrapper> master , IEnumerable<FileWrapper> slave)
        {
            foreach(var file in master)
            {
                CreateAllDirsForFile(file.RelativePath , SlavePath);
                File.Copy(Path.Combine(MasterPath , file.RelativePath) , Path.Combine(SlavePath , file.RelativePath));
                _logger.AddCopy(file.RelativePath , MasterPath , SlavePath);
            }
            foreach(var file in slave)
            {
                CreateAllDirsForFile(file.RelativePath , MasterPath);
                File.Copy(Path.Combine(SlavePath , file.RelativePath) , Path.Combine(MasterPath , file.RelativePath));
                _logger.AddCopy(file.RelativePath , SlavePath , MasterPath);
            }
        }

        public void CreateAllDirsForFile(string path , string root)
        {
            string current = root;
            string[] folders = path.Split(new char[] { '\\' });
            for(int i = 0; i < folders.Length - 1; i++)
            {
                current = Path.Combine(current , folders[i]);
                if(!Directory.Exists(current))
                    Directory.CreateDirectory(current);
            }
        }
    }
}
