using System.Collections.Generic;
using System.IO;

namespace FileLib
{
    // todo: *done* IDirectory ?
    public class Mp3Directory : BaseDirectory
    {
        protected override bool Exists(string path)
        {
            return File.Exists(path);
        }

        public override IEnumerable<IMp3File> GetFiles(string directory)
        {
            List<IMp3File> mp3Files = new List<IMp3File>();
            foreach (var item in GetFileNames(directory))
            {
                mp3Files.Add(new Mp3File(TagLib.File.Create(item)));
            }

            return mp3Files;
        }

        private IEnumerable<string> GetFileNames(string directory)
        {
            return Directory.GetFiles(directory, "*.mp3", SearchOption.TopDirectoryOnly);
        }
    }
}