using System;
using System.IO;

namespace ToDoListConsole
{
    static class FirstExecutionDeterminer
    {
        public static bool FirstRun { get; private set; }

        public static void DetermineWhetherItIsAFirstRun()
        {
            var path = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "FirstRun.txt");
            if (File.Exists(path))
            {
                FirstRun = false;
            }
            else
            {
                CreateEmptyFile(path);
            }
        }

        static void CreateEmptyFile(string fileName)
        {
            File.Create(fileName).Dispose();
            File.SetAttributes(fileName, File.GetAttributes(fileName) | FileAttributes.Hidden);
        }
    }
}
