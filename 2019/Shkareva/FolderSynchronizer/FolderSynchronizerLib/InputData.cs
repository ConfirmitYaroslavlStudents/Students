namespace FolderSynchronizerLib
{
    public class InputData
    {
        public string MasterPath;
        public string SlavePath;
        public bool NoDeleteFlag;
        public string LogFlag;

        public InputData()
        {
            NoDeleteFlag = false;
            LogFlag = "summary";
        }
    }
}
