using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Abstractions;

namespace RenamerLib
{
    public interface IFileManager
    {
        bool Exist(string path);
        void Move(string source, string dest);
        IEnumerable<IMP3File> GetFiles(string searchPattern, SearchOption searchOption);
    }

    public class FileManager : IFileManager
    {
        public bool Exist(string path) => File.Exists(path);

        public void Move(string source, string dest) => File.Move(source, dest);

        public IEnumerable<IMP3File> GetFiles(string searchPattern, SearchOption searchOption)
        {
            IEnumerable<string> fileNames = Directory.GetFiles(Directory.GetCurrentDirectory(),
                searchPattern, searchOption);
            return fileNames.Select(file => new MP3File(file)).ToArray();
        }
    }
}
