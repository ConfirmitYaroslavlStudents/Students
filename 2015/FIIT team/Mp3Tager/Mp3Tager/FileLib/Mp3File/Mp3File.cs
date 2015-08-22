using System;
using System.IO;

namespace FileLib
{
    public class Mp3File : IMp3File
    {
        private readonly TagLib.File _content;

        public Mp3Tags Tags { get; private set; }

        public string FullName { get; private set; }

        private string _newFullName;

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
            _content.Save();
            if (_newFullName != null)
            {
                MoveTo(_newFullName);
            }
        }

        public void SaveWithBackup()
        {
            // todo: rename --backup-ignore "" {title}
            // todo: tumbler to switch off backup process
            using (var backup = new FileBackuper(this))
            {
                try
                {
                    Save();
                }
                catch (Exception e)
                {
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

        public IMp3File CopyTo(string uniquePath)
        {
            File.Copy(FullName, uniquePath, true);
            return new Mp3File(TagLib.File.Create(uniquePath));
        }

        public void Delete()
        {
            File.Delete(FullName);
        }

        public void MoveTo(string uniquePath)
        {
            File.Move(FullName, uniquePath);
            FullName = uniquePath;
            // TODO: what happen with _content?
        }

        public void MoveTo(string uniquePath, bool safe)
        {
            _newFullName = uniquePath;
        }
    }
}