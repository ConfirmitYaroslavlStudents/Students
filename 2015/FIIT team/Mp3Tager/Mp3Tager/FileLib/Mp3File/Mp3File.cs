using System.IO;
using TagLib;
using System;

namespace FileLib
{
    public class Mp3File : IMp3File
    {
        private readonly TagLib.File _content;

        public Mp3Tags Tags { get; private set; }

        public string FullName { get; private set; }

        public Mp3File(TagLib.File mp3Content)
        {           
            _content = mp3Content;
            FullName = mp3Content.Name;         

            Tags = new Mp3Tags  
            {
                Album = mp3Content.Tag.Album,
                Title = mp3Content.Tag.Title,                  
                Artist = mp3Content.Tag.FirstPerformer,
                Genre = mp3Content.Tag.FirstGenre,
                Track = mp3Content.Tag.Track
            };
        }        

        public void Save()
        {
            SaveTags();
            using (var backup = new FileBackuper())
            {
                backup.MakeBackup(this);
                try
                {
                    _content.Save();
                }
                catch(Exception e)
                {
                    // todo: user unable to get full info about the process if smth get wrong
                    backup.RestoreFromBackup();
                    throw new Exception("File was restored from backup because of exception:", e);
                }
            }
        }

        private void SaveTags()
        {            
            if (Tags.Artist != null)
            {
                _content.Tag.Performers = null;
                _content.Tag.Performers = new[] { Tags.Artist };
            }
            if (Tags.Genre != null)
            {
                _content.Tag.Genres = null;
                _content.Tag.Genres = new[] { Tags.Genre };
            }
            _content.Tag.Title = Tags.Title;
            _content.Tag.Album = Tags.Album;
            _content.Tag.Track = Tags.Track;
        }        

        public IMp3File CopyTo(string path)
        {
            System.IO.File.Copy(FullName, path, true);
            return new Mp3File(TagLib.File.Create(path));
        }

        public void Delete()
        {
            System.IO.File.Delete(FullName);
        }

        public void MoveTo(BaseFileExistenceChecker checker, string path)
        {
            var destinationPath = checker.CreateUniqueName(path);
            MoveFileWithBackUp(destinationPath);
        }

        private void MoveFileWithBackUp(string destinationPath)
        {
            using (var backup = new FileBackuper())
            {
                backup.MakeBackup(this);
                try
                {
                    System.IO.File.Move(FullName, destinationPath);
                    FullName = destinationPath;
                }
                catch (Exception e)
                {
                    backup.RestoreFromBackup();
                    throw new Exception("File was restored from backup because of exception:", e);
                }
            }
        }
    }
}
