using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicFileRenameLibrary
{
    public class Parser
    {
        public FileShell ParseFile(string filePath)
        {
            var parsedFile = new FileShell();
            parsedFile.FullFilePath = filePath;

            var pathLength = filePath.LastIndexOf(@"\") + 1;
            var path = filePath.Substring(0, pathLength);
            var name = filePath.Substring(pathLength);
            parsedFile.Path = path;

            var extensionLength = 4;
            var extensionIndex = name.Length - extensionLength;
            var extension = name.Substring(extensionIndex);
            parsedFile.Extension = extension;

            var dashInNameIndex = name.IndexOf('-');

            var artistLength = dashInNameIndex;
            var artist = name.Substring(0,artistLength);
            parsedFile.Artist = artist;

            var titleLength = name.Length - dashInNameIndex - extensionLength - 1;
            var titleIndex = dashInNameIndex + 1;
            var title = name.Substring(titleIndex, titleLength);
            parsedFile.Title = title;
            
            return parsedFile;
        }

        public string GetFileExtension(string filePath)
        {
            if (filePath.Length < 4)
                throw new ArgumentException("Маска не может быть короче 4 символов");
            var extensionIndex = filePath.Length - 4;
            return filePath.Substring(extensionIndex);
        }

        public void CollectFilePath(FileShell parsedFile)
        {
            parsedFile.FullFilePath = parsedFile.Path + parsedFile.Artist + "-" + parsedFile.Title + parsedFile.Extension;
        }
    }
}
