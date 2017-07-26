using System;

namespace TaggerLib
{
    internal class File
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

        private void SaveTags()
        {
            if (Performers == null || Title == null)
                throw new ArgumentNullException();

            var file = TagLib.File.Create(Path);
            file.Tag.Performers = Performers;
            file.Tag.Title = Title;
            file.Save();
        }

        private string NewPath()
        {
            if (Name == null)
                throw new ArgumentNullException();
            var dirPath = System.IO.Path.GetDirectoryName(Path);
            var newPath = System.IO.Path.Combine(dirPath, Name);

            return newPath;
        }

        private void SaveName()
        {
            System.IO.File.Move(Path, NewPath());
        }

        public void Save()
        {
            SaveTags();
            SaveName();
        }
    }
}