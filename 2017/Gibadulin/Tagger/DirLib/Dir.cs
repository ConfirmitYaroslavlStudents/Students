using System;
using System.IO;
using ParseInputLib;
using TagsLib;

namespace DirLib
{
    public class Dir
    {
        public static void ChangeFiles(ParseInput.InputData inputData)
        {           
            try
            {
                string[] filesToChange;
                if (inputData.Subfolders)
                    filesToChange = Directory.GetFiles(inputData.Path, inputData.Mask, SearchOption.AllDirectories);
                else
                    filesToChange = Directory.GetFiles(inputData.Path, inputData.Mask);

                if (inputData.Modifier == "-t")
                    FilesToTag(filesToChange);
                if (inputData.Modifier == "-n")
                    FilesToName(filesToChange);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }     
        }

        private static void FilesToTag(string[] filesToChange)
        {
            foreach (var item in filesToChange)
                Tags.ToTag(item);
        }

        private static void FilesToName(string[] filesToChange)
        {
            foreach (var item in filesToChange)
                Tags.ToName(item);
        }
    }
}
