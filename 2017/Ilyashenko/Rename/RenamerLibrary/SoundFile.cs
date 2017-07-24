namespace RenamerLibrary
{
    public abstract class SoundFile
    {
        public string path;
        public string Artist;
        public string Title;

        public abstract void MakeFilename();
        public abstract void MakeTags();
    }
}
