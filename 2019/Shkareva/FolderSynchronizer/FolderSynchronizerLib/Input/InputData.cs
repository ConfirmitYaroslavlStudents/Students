using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public class InputData
    {
        public List<string> FoldersPaths;
        public bool NoDeleteFlag;
        public string LogFlag;

        public InputData()
        {
            FoldersPaths = new List<string>();
            NoDeleteFlag = false;
            LogFlag = "summary";
        }
    }
}
