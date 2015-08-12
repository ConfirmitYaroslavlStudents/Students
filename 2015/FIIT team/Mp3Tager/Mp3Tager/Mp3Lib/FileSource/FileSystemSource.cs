using System.Collections.Generic;
using System.IO;

namespace Mp3Lib
{
    public class FileSystemSource : ISource
    {

        public FileSystemSource(string source)
        {
            SourceFolder = source;
        }

        public string SourceFolder { get; private set; }
        public IEnumerable<string> GetFileNames()
        {
            return Directory.GetFiles(SourceFolder, "*.mp3", SearchOption.TopDirectoryOnly);
        }

        public IEnumerable<IMp3File> GetFiles()
        {
            List<IMp3File> mp3Files = new List<IMp3File>();
            foreach (var item in GetFileNames())
            {
                mp3Files.Add(new Mp3File(TagLib.File.Create(item)));
            }

            return mp3Files;
        }
    }
}
