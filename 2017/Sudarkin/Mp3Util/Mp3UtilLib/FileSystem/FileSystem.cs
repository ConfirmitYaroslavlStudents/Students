using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mp3UtilLib.FileSystem
{
    public class FileSystem : IFileSystem
    {
        public bool Exists(string path) => File.Exists(path);

        public void Move(string source, string dest)
        {
            if (Exists(dest))
            {
                throw new IOException($"{dest} - File is already exists!");
            }

            File.Move(source, dest);
        }

        public IEnumerable<AudioFile> GetAudioFilesFromCurrentDirectory(string searchPattern, bool recursive)
        {
            IEnumerable<string> files = Directory.GetFiles(
                Directory.GetCurrentDirectory(),
                searchPattern,
                recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            return files.Select(file => new Mp3File(file)).ToArray();
        }
    }
}