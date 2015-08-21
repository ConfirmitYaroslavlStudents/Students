using System.Collections.Generic;
using System.IO;

namespace FileLib
{
    // todo: *done* IDirectory ?
    public class Mp3Directory : IDirectory
    {
        public Mp3Directory(string source)
        {
            SourceFolder = source;
        }

        public string SourceFolder { get; private set; }

        public IEnumerable<IMp3File> GetFiles()
        {
            List<IMp3File> mp3Files = new List<IMp3File>();
            foreach (var item in GetFileNames())
            {
                mp3Files.Add(new Mp3File(TagLib.File.Create(item)));
            }

            return mp3Files;
        }
                
        private IEnumerable<string> GetFileNames()
        {
            return Directory.GetFiles(SourceFolder, "*.mp3", SearchOption.TopDirectoryOnly);
        }
    }
}