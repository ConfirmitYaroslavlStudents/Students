namespace SynchLibrary
{
    public class Sync
    {
        string MasterPath { get; set; }
        string SlavePath { get; set; }

        public Sync(string master, string slave)
        {
            MasterPath = master;
            SlavePath = slave;
        }

        public void Synchronization()
        {
            FileHandler files = new FileHandler(MasterPath, SlavePath, TypesOfLogging.Summary);
            files.MigrateForFiles();
        }
    }
}
