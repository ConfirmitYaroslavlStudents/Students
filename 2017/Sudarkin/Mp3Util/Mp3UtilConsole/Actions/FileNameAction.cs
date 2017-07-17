using System.IO;
using System.IO.Abstractions;

namespace Mp3UtilConsole.Actions
{
    public class FileNameAction : IActionStrategy
    {
        private readonly FileManager _fileManager;

        public FileNameAction() : this(new FileSystem())
        {
            
        }

        public FileNameAction(IFileSystem fileSystem)
        {
            _fileManager = new FileManager(fileSystem);
        }

        public void Process(Mp3File mp3File)
        {
            string newFileName = Path.Combine(Path.GetDirectoryName(mp3File.FullName), 
                $"{mp3File.Artist} - {mp3File.Title}.mp3");

            if (_fileManager.Exists(newFileName))
            {
                throw new IOException($"{newFileName} - File is already exists!");
            }

            _fileManager.Move(mp3File.FullName, newFileName);

            mp3File.FullName = newFileName;
        }
    }
}