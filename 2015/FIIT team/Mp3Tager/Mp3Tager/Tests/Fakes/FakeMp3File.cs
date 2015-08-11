using System.Collections.Generic;
using Mp3Lib;

namespace Tests
{
    public class FakeMp3File : IMp3File
    {
        public string Path { get; internal set; }
        public IMp3Tags Mp3Tags { get; private set; }

        private readonly HashSet<string> _paths = new HashSet<string>
        {
            @"D:\music\Alla.mp3", @"D:\music\Alla (1).mp3"
        };
        

        public FakeMp3File(string path, IMp3Tags tags)
        {
            Path = path;
            Mp3Tags = tags;
        }

        public void MoveTo(string path)
        {
            var directory = System.IO.Path.GetDirectoryName(path);
            var fileName = System.IO.Path.GetFileNameWithoutExtension(path);
            var destinationPath = System.IO.Path.Combine(directory, fileName + @".mp3");

            var index = 1;

            while (_paths.Contains(destinationPath))
            {
                destinationPath = System.IO.Path.Combine(directory, fileName + @" (" + index + ").mp3");
                index++;
            }

            Path = destinationPath;
        }

        public void Save()
        {

        }
    }
}
