namespace MusicFileRenameLibrary
{
    public class FileNameRenamer : IRenamer
    {
        public void Rename(FileShell parsedFile)
        {
            parsedFile.Artist = parsedFile.TagArtist;
            parsedFile.Title = parsedFile.TagTitle;
        }
    }
}
