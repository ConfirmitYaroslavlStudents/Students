namespace MusicFileRenameLibrary
{
    public class FileShell
    {
        public string FullFilePath { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string TagArtist { get; set; }
        public string TagTitle { get; set; }

        public FileShell(string fullPath = "", string path = "", string extension = "",
            string artist = "", string title = "",
            string tagArtist = "", string tagTitle = "")
        {
            FullFilePath = fullPath;
            Path = path;
            Extension = extension;
            Artist = artist;
            Title = title;
            TagArtist = tagArtist;
            TagTitle = tagTitle;
        }
    }
}
