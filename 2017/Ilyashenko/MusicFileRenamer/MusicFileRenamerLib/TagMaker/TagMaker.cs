using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace MusicFileRenamerLib
{
    public class TagMaker : ITagMaker
    {
        public void MakeTags(Mp3File file)
        {
            var fileName = Path.GetFileNameWithoutExtension(file.path);
            var nameParts = Regex.Split(fileName, " - ").Where(x => !String.IsNullOrEmpty(x)).ToArray();

            if (nameParts.Length != 2)
            {
                throw new ArgumentException("Недопустимое имя файла. Имя файла должно иметь вид \"Исполнитель - Название песни.mp3\".");
            }
            SetTags(file, nameParts[0], nameParts[1]);
        }

        private void SetTags(Mp3File file, string artist, string title)
        {
            file.Artist = artist;
            file.Title = title;

            var taggedFile = TagLib.File.Create(file.path);
            taggedFile.Tag.Performers = new string[] { artist };
            taggedFile.Tag.Title = title;
            taggedFile.Save();
        }
    }
}
