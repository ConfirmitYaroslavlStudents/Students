namespace MusicFileRenameLibrary
{
    public class TagRenamer : IRenamer
    {
        public void Rename(FileShell parsedFile)
        {
            parsedFile.TagArtist = parsedFile.Artist;
            parsedFile.TagTitle = parsedFile.Title;
        }
    }
}
