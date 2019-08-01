namespace SynchLibrary
{
    public partial class Merge
    {
        string MasterPath { get; set; }
        string SlavePath { get; set; }

        public Merge(string master, string slave)
        {
            MasterPath = master;
            SlavePath = slave;
        }


        public void Working()
        {
            FolderHandler folders = new FolderHandler(MasterPath, SlavePath);
            folders.MigrateForFolders();
            FileHandler files = new FileHandler(MasterPath, SlavePath);
            files.MigrateForFiles();
        }
    }
}
