using System.IO;
using TagLib;
using FileBackuperLib;
using System;

namespace Mp3Lib
{
    public class Mp3File : IMp3File
    {
        private readonly TagLib.File _file;

        public string Path { get; private set; }

        public IMp3Tags Mp3Tags { get; private set; }

        public Mp3File(TagLib.File file)
        {
            _file = file;
            Path = _file.Name;

            Mp3Tags = new Mp3Tags  
            {
                Album = file.Tag.Album,
                Title = file.Tag.Title,
                Artist = file.Tag.Performers[0],
                Genre = file.Tag.Genres[0],
                Track = file.Tag.Track
            };
        }

        public void MoveTo(string path)
        {            
            var directory = System.IO.Path.GetDirectoryName(path);
            var fileName = System.IO.Path.GetFileNameWithoutExtension(path);
            var destinationPath = System.IO.Path.Combine(directory, fileName + @".mp3");

            var index = 1;

            // TODO: work with true file system?
            while (System.IO.File.Exists(destinationPath))
            {
                destinationPath = System.IO.Path.Combine(directory, fileName + @" (" + index + ").mp3");
                index++;
            }

            new FileInfo(Path).MoveTo(destinationPath);
            Path = destinationPath;
        }

        public void Save()
        {
            using (var backup = new FileBackuper())
            {
                backup.MakeBackup(new FileBackuperLib.File(Path));

                try
                {
                    _file.Save();
                }
                catch(Exception e)
                {
                    // todo: user unable to get full info about the process if smth get wrong
                    backup.RestoreFromBackup();
                }
            }
        }
    }
}
