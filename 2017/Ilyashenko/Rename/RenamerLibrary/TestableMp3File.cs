using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace RenamerLibrary
{
    public class TestableMp3File : SoundFile
    {
        private string _artist;
        private string _title;

        public new string Artist
        {
            get
            {
                return _artist;
            }
            set
            {
                _artist = value;
            }
        }

        public new string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        public TestableMp3File(string _path)
        {
            path = _path;
        }

        public override void MakeFilename()
        {
            var newPath = Path.GetDirectoryName(path) + "\\" + Artist + " - " + Title + Path.GetExtension(path);
            path = newPath;
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
