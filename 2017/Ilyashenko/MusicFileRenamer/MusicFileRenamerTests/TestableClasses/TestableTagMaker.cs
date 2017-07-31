using System;
using System.Linq;
using MusicFileRenamerLib;
using System.IO;
using System.Text.RegularExpressions;

namespace MusicFileRenamerTests
{
    public class TestableTagMaker : ITagMaker
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
        }
    }
}
