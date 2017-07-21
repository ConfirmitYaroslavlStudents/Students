using System;
using System.IO;

namespace TaggerLib
{
    public class Tag
    {
        private static string NewFileName(string path)
        {
            var file = TagLib.File.Create(path);
            if (file.Tag.Performers == null)
                throw new ArgumentNullException();
            if (file.Tag.Title == null)
                throw new ArgumentNullException();

            var newFileName = string.Join(", ", file.Tag.Performers) + " - " + file.Tag.Title + Path.GetExtension(path);

            return newFileName;
        }

        private static string NewFileFullPath(string path)
        {
            var newFileName = NewFileName(path);
            var newFileFullPath = Path.Combine(Path.GetDirectoryName(path), newFileName);

            return newFileFullPath;
        }

        public static void ToName(string path)
        {
            var newFileFullPath = NewFileFullPath(path);
            File.Move(path, newFileFullPath);
        }

        private static string[] NewTags(string path) //поменять string[] на class
        {
            var tags = Path.GetFileNameWithoutExtension(path)
                .Split(new[] {" - "}, StringSplitOptions.RemoveEmptyEntries);
            if (tags.Length != 2)
                throw new ArgumentException();

            return tags;
        }

        public static void ToTag(string path)
        {
            var tags = NewTags(path);

            var file = TagLib.File.Create(path);
            file.Tag.Performers = tags[0].Split(new[] {", "}, StringSplitOptions.RemoveEmptyEntries);
            file.Tag.Title = tags[1];

            file.Save();
        }
    }
}
