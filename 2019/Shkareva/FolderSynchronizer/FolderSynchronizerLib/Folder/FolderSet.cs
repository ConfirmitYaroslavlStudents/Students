namespace FolderSynchronizerLib
{
    public class FolderSet
    {
        public Folder OldMaster;
        public Folder OldSlave;
        public Folder NewMaster;
        public Folder NewSlave;
        public bool NoDeleteFlag;
        public string Loglevel;

        public FolderSet(InputData input)
        {
            var folderWorker = new FolderWorker();
            NewMaster = folderWorker.LoadFolder(input.MasterPath);
            NewSlave = folderWorker.LoadFolder(input.SlavePath);
            OldMaster = folderWorker.LoadSerializedFolder(input.MasterPath);
            OldSlave = folderWorker.LoadSerializedFolder(input.SlavePath);
            NoDeleteFlag = input.NoDeleteFlag;
            Loglevel = input.LogFlag;
        }

        public FolderSet(Folder oldM,Folder oldS,Folder newM,Folder newS, bool flag, string level)
        {
            NewMaster = newM;
            NewSlave = newS;
            OldMaster = oldM;
            OldSlave = oldS;
            NoDeleteFlag = flag;
            Loglevel = level;
        }
    }
}
