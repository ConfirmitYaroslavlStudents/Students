using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace MusicFileRenamerLib
{
    public class FileProcessor : IFileProcessor
    {
        private IFileSystem _fileSystem;

        public FileProcessor(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        public void MakeTags(Mp3File file)
        {
            var fileName = Path.GetFileNameWithoutExtension(file.path);
            var nameParts = Regex.Split(fileName, " - ").Where(x => !String.IsNullOrEmpty(x)).ToArray();

            if (nameParts.Length != 2)
            {
                throw new ArgumentException("Недопустимое имя файла. Имя файла должно иметь вид \"Исполнитель - Название песни.mp3\".");
            }

            file.Artist = nameParts[0];
            file.Title = nameParts[1];

            _fileSystem.SetTags(file);
        }

        public void MakeFilename(Mp3File file)
        {
            var newPath = Path.GetDirectoryName(file.path) + "\\" + file.Artist + " - " + file.Title + Path.GetExtension(file.path);
            if (!_fileSystem.Exists(newPath))
            {
                _fileSystem.Move(file.path, newPath);
                file.path = newPath;
            }
        }
    }
}
