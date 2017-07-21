using System;

namespace TaggerLib
{
    public class ParseInput
    {
        public class InputData
        {
            public string Path { get; internal set; }
            public string Mask { get; internal set; }
            public string Modifier { get; internal set; }
            public bool Subfolders { get; internal set; }
        }

        public static InputData Parse(string[] args)
        {
            if (args.Length > 4 || args.Length < 3)
            {
                throw new ArgumentException("Wrong Input");
            }
            if (args[2] != Consts.ToName && args[2] != Consts.ToTag)
            {
                throw new ArgumentException("Wrong Input");
            }
            if (args.Length == 4 && args[3] != Consts.Subfolder)
            {
                throw new ArgumentException("Wrong Input");
            }

            var inputData = new InputData();
            inputData.Path = args[0];
            inputData.Mask = args[1];
            inputData.Modifier = args[2];
            if (args.Length == 4)
                inputData.Subfolders = true;
            else
                inputData.Subfolders = false;

            return inputData;
        }
    }
}
