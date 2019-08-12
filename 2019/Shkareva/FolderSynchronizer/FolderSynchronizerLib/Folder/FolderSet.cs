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
            NewMaster = new FolderWorker().LoadFolder(input.MasterPath);
            NewSlave = new FolderWorker().LoadFolder(input.SlavePath);
            OldMaster = new FolderWorker().LoadSerializedFolder(input.MasterPath);
            OldSlave = new FolderWorker().LoadSerializedFolder(input.SlavePath);
            NoDeleteFlag = input.NoDeleteFlag;
            Loglevel = input.LogFlag;
        }
    }
}
