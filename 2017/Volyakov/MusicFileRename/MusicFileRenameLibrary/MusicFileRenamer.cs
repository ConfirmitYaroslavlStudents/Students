using System;
using System.IO;

namespace MusicFileRename
{
    //SRP violation
    public static class MusicFileRenamer
    {

        public static void RenameFileNameByTag(string pattern, bool recursive = false, string directory = null)
        {
            var files = GetFiles(pattern,recursive,directory);
            foreach (var filePath in files)
            {
                var slashIndex = filePath.LastIndexOf(@"\");
                var pathWithoutName = filePath.Substring(0, slashIndex + 1);
                var fileTag = TagLib.File.Create(filePath);

                var newName = fileTag.Tag.Performers[0] + "-" + fileTag.Tag.Title + ".mp3";
                var newPath = pathWithoutName + newName;

                if(!File.Exists(newPath))
                    File.Move(filePath,newPath);
            }
        }

        public static void RenameTagByFileName(string pattern, bool recursive = false, string directory = null)
        {
            var files = GetFiles(pattern, recursive,directory);
            foreach (var filePath in files)
            {
                var nameStartIndex = filePath.LastIndexOf(@"\") + 1;
                var fileName = filePath.Substring(nameStartIndex, filePath.Length - nameStartIndex);

                var dashIndex = fileName.IndexOf('-');
                var performerLength = dashIndex;
                var titleLength = fileName.Length - dashIndex - 5;
                var performer = fileName.Substring(0, performerLength);
                var title = fileName.Substring(dashIndex + 1, titleLength);

                var fileTag = TagLib.File.Create(filePath);
                fileTag.Tag.Performers = new string[] { performer };
                fileTag.Tag.Title = title;
                fileTag.Save();
            }
        }

        private static string[] GetFiles(string pattern, bool recursive, string directory)
        {
            var IsMp3 = pattern.IndexOf(".mp3") == pattern.Length - 4;
            if (pattern.Length < 5 || ! IsMp3)
                throw new ArgumentException("Wrong Pattern");
            
            SearchOption option;
            if (recursive)
                option = SearchOption.AllDirectories;
            else
                option = SearchOption.TopDirectoryOnly;

            if (directory == null)
                directory = Directory.GetCurrentDirectory();
            else if (!Directory.Exists(directory))
                throw new ArgumentException("Wrong Directory");

            return Directory.GetFiles(directory,pattern,option);
        }

    }
}
