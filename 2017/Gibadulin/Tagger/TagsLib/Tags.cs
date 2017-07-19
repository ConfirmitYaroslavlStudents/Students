using System;
using System.IO;

namespace TagsLib
{
    public class Tags
    {
        public static void ToName(string path)
        {
            try
            {
                var file = TagLib.File.Create(path);
                if (file.Tag.Performers == null)
                    throw new ArgumentNullException();
                if (file.Tag.Title == null)
                    throw new ArgumentNullException();

                var newFileName = string.Join(", ", file.Tag.Performers) + " - " + file.Tag.Title + Path.GetExtension(path);
                var newFileFullPath = Path.Combine(Path.GetDirectoryName(path), newFileName);

                File.Move(path, newFileFullPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void ToTag(string path)
        {
            try
            {
                var tags = Path.GetFileNameWithoutExtension(path).Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                if (tags.Length != 2)
                    throw new ArgumentException();
                var file = TagLib.File.Create(path);

                file.Tag.Performers = tags[0].Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                file.Tag.Title = tags[1];
                file.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
