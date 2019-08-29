namespace FolderSynchronizerLib
{
    public class FolderPair
    {
        public readonly Folder Old;
        public readonly Folder New;

        public FolderPair(Folder oldFolder, Folder newFolder)
        {
            Old = oldFolder;
            New = newFolder;
        }
    }
}
