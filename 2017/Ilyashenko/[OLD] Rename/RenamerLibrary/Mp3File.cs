using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace RenamerLibrary
{
    public class Mp3File : SoundFile
    {
        private TagLib.File file;

        public new string Artist
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

        public new string Title
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

        public override void MakeFilename()
        {
            var newPath = Path.GetDirectoryName(path) + "\\" + Artist + " - " + Title + Path.GetExtension(path);
            if (!File.Exists(newPath))
            {
                File.Move(path, newPath);
                path = newPath;
                file = TagLib.File.Create(path);
            }
        }

        public override void MakeTags()
        {
            var fileName = Path.GetFileNameWithoutExtension(path);
            var nameParts = Regex.Split(fileName, " - ").Where(x => !String.IsNullOrEmpty(x)).ToArray();

            if (nameParts.Length != 2)
            {
                throw new ArgumentException("Недопустимое имя файла. Имя файла должно иметь вид \"Исполнитель - Название песни.mp3\".");
            }

            Artist = nameParts[0];
            Title = nameParts[1];
        }
    }
}
