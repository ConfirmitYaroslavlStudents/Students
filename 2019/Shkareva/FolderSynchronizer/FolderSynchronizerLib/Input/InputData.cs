using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public class InputData
    {
        public List<string> FoldersPaths;
        public bool NoDeleteFlag;
        public LogLevels LogLevel;

        public InputData()
        {
            FoldersPaths = new List<string>();
            NoDeleteFlag = false;
            LogLevel = LogLevels.summary;
        }
    }
}
