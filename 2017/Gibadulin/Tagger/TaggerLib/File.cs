using System;

namespace TaggerLib
{
    public class File
    {
        public string Name { get; set; }
        public string Path { get; }

        public string[] Performers { get; set; }
        public string Title { get; set; }

        public File(string path)
        {
            Path = path;
            Name = System.IO.Path.GetFileNameWithoutExtension(path);
            Performers = null;
            Title = null;
        }

        public void GetTags()
        {
            var file = TagLib.File.Create(Path);
            Performers = file.Tag.Performers;
            Title = file.Tag.Title;
        }

        public void Save()
        {
            SaveTags();
            SaveName();
        }

        internal void SaveTags()
        {
            if (Performers == null || Title == null)
                throw new ArgumentNullException();

            var file = TagLib.File.Create(Path);
            file.Tag.Performers = Performers;
            file.Tag.Title = Title;
            file.Save();
        }

        internal void SaveName()
        {
            System.IO.File.Move(Path, NewPath());
        }

        internal string NewPath()
        {
            if (Name == null)
                throw new ArgumentNullException();

            var dirPath = System.IO.Path.GetDirectoryName(Path);
            if (dirPath==null)
                throw new ArgumentNullException();

            var newPath = System.IO.Path.Combine(dirPath, Name);

            return newPath;
        }
    }
}