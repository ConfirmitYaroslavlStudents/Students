using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLib.FileSource
{
    public class FileSource : IFileSource
    {
        public IEnumerable<IMp3File> GetFiles(string path)
        {
            var files = new List<IMp3File>();
            if (Path.GetExtension(path) != string.Empty)
                files.Add(new Mp3File(path));
            else
                files.AddRange(GetFilesFromDirectory(path));
            return files;
        }

        private IEnumerable<IMp3File> GetFilesFromDirectory(string directory)
        {
            return Directory.GetFiles(directory, "*.mp3", SearchOption.TopDirectoryOnly).Select(name => new Mp3File(name)).Cast<IMp3File>().ToList();
        }
    }
}
