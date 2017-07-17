using System.IO;
using TagLib;
using TagFile = TagLib.File;

namespace Mp3UtilConsole
{
    public class Mp3File
    {
        private TagFile _file;

        public string Artist { get; set; }
        public string Title { get; set; }

        public string FullName { get; set; }

        public Mp3File(string path)
        {
            try
            {
                Init(TagFile.Create(path));
            }
            catch (UnsupportedFormatException)
            {
                throw new FileLoadException($"{path} - This file is not mp3!");
            }
        }

        public Mp3File(TagFile tagFile)
        {
            Init(tagFile);
        }

        private void Init(TagFile tagFile)
        {
            if (tagFile.MimeType != "taglib/mp3")
            {
                throw new FileLoadException($"{tagFile.Name} - This file is not mp3!");
            }

            _file = tagFile;

            FullName = tagFile.Name;

            Artist = _file.Tag.FirstPerformer;
            Title = _file.Tag.Title;
        }

        public void Save()
        {
            _file.Tag.Performers = new[] {Artist};
            _file.Tag.Title = Title;
            _file.Save();
        }
    }
}