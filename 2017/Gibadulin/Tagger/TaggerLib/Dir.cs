using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaggerLib
{
    public class Dir
    {
        public static void ChangeFiles(ParseInput.InputData inputData)
        {
                string[] filesToChange;
                if (inputData.Subfolders)
                    filesToChange = Directory.GetFiles(inputData.Path, inputData.Mask, SearchOption.AllDirectories);
                else
                    filesToChange = Directory.GetFiles(inputData.Path, inputData.Mask);

                if (inputData.Modifier == Consts.ToTag)
                    FilesToTag(filesToChange);
                if (inputData.Modifier == Consts.ToName)
                    FilesToName(filesToChange);
        }

        private static void FilesToTag(string[] filesToChange)
        {
            foreach (var item in filesToChange)
                Tag.ToTag(item);
        }

        private static void FilesToName(string[] filesToChange)
        {
            foreach (var item in filesToChange)
                Tag.ToName(item);
        }
    }
}
