using System;
using System.IO;
namespace SynchLibrary
{
    public class Sync
    {
        public string MasterPath { get; private set; }
        public string SlavePath { get; private set; }
        public bool CanRemove { get; private set; }
        public TypesOfLogging Type { get; private set; }

        public Sync(string master, string slave, bool mode, int type)
        {
            var absMaster = new DirectoryInfo(master).FullName;
            MasterPath = absMaster;
            var absSlave = new DirectoryInfo(slave).FullName;
            SlavePath = absSlave;
            CanRemove = mode;
            if (type >= 0 && type < 3)
                Type = (TypesOfLogging)type;
            else
                throw new FormatException("Incorrect key for logging");
        }

        public void Synchronization()
        {
            FileHandler files = new FileHandler(this);
            files.MigrateForFiles();
        }
    }
}
