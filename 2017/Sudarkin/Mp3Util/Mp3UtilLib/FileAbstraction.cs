using System.IO;
using File = TagLib.File;

namespace Mp3UtilLib
{
    public class FileAbstraction : File.IFileAbstraction
    {
        public string Name { get; }
        public Stream ReadStream { get; }
        public Stream WriteStream => ReadStream;

        public FileAbstraction(string name, Stream stream)
        {
            Name = name;
            ReadStream = stream;
        }

        public void CloseStream(Stream stream)
        {
            stream.Position = 0;
        }
    }
}