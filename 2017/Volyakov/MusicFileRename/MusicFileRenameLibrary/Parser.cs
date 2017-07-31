using System;

namespace MusicFileRenameLibrary
{
    public class Parser
    {
        public string[] ParseFile(string filePath, string tagArtist = "", string tagTitle = "")
        {
            if (filePath.Length < 5)
                throw new ArgumentException("Wrong length of file path");

            var parsedFile = new string[7];
            parsedFile[0] = filePath;

            var pathLength = filePath.LastIndexOf(@"\") + 1;
            var path = filePath.Substring(0, pathLength);
            var name = filePath.Substring(pathLength);
            parsedFile[1] = path;

            var extensionLength = 4;
            var extensionIndex = name.Length - extensionLength;
            var extension = name.Substring(extensionIndex);
            parsedFile[2] = extension;

            var dashInNameIndex = name.IndexOf('-');

            var artistLength = dashInNameIndex;
            if (artistLength == -1)
                artistLength = name.Length - 4;
            var artist = name.Substring(0, artistLength);
            parsedFile[3] = artist;

            var titleLength = name.Length - dashInNameIndex - extensionLength - 1;
            var titleIndex = dashInNameIndex + 1;
            var title = name.Substring(titleIndex, titleLength);
            parsedFile[4] = title;

            parsedFile[5] = tagArtist;
            parsedFile[6] = tagTitle;

            return parsedFile;
        }
        
        public void CollectFilePath(FileShell parsedFile)
        {
            parsedFile.FullFilePath = parsedFile.Path + parsedFile.Artist + "-" + parsedFile.Title + parsedFile.Extension;
        }
    }
}
