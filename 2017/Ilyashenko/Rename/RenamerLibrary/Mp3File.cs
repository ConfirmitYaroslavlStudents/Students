using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace RenamerLibrary
{
    public class Mp3File
    {
        private string path { get; set; }
        private TagLib.File file;

        public string Artist
        {
            get
            {
                var artists = new string[] { file.Tag.Artists[0], file.Tag.FirstArtist, file.Tag.AlbumArtists[0], file.Tag.FirstAlbumArtist, file.Tag.Performers[0], file.Tag.FirstPerformer, file.Tag.JoinedArtists, file.Tag.JoinedPerformers, file.Tag.JoinedAlbumArtists };
                return artists.Where(x => !String.IsNullOrEmpty(x)).First();
            }
            set
            {
                file.Tag.Performers = new string[] { value };
                file.Save();
            }
        }

        public string Title
        {
            get
            {
                return file.Tag.Title;
            }
            set
            {
                file.Tag.Title = value;
                file.Save();
            }
        }

        public Mp3File(string _path)
        {
            path = _path;
            file = TagLib.File.Create(path);
        }

        public void MakeFilename()
        {
            var newPath = Path.GetDirectoryName(path) + "\\" + Artist + " - " + Title + Path.GetExtension(path);
            if (!File.Exists(newPath))
            {
                File.Move(path, newPath);
                path = newPath;
                file = TagLib.File.Create(path);
            }
        }

        public void MakeTags()
        {
            var fileName = Path.GetFileNameWithoutExtension(path);
            var nameParts = Regex.Split(fileName, " - ").Where(x => !String.IsNullOrEmpty(x)).ToArray();

            if (nameParts.Length != 2)
            {
                throw new ArgumentException("Недопустимое имя файла. Имя файла должно иметь вид \"Исполнитель - Название песни.mp3\".");
            }

            SetTags(nameParts);
        }

        public void SetTags(string[] tags)
        {
            Artist = tags[0];
            Title = tags[1];
        }
    }
}
