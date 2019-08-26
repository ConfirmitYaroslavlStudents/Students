using System;
using System.IO;

namespace GeneralizeSynchLibrary
{
    public class FileHandler
    {
        public static void Remove(FileWrapper file, ILogger logger)
        {
            File.Delete(Path.Combine(file.Root, file.Path));
            logger.AddRemove(Path.Combine(file.Root, file.Path));
        }

        public static void Replace(Tuple<FileWrapper, FileWrapper> replaceObj, ILogger logger)
        {
            string input = Path.Combine(replaceObj.Item1.Root, replaceObj.Item1.Path);
            string output = Path.Combine(replaceObj.Item2.Root, replaceObj.Item2.Path);
            File.Copy(input, output, true);
            logger.AddReplace(input, output);
        }

        public static bool AreNotEqual(FileWrapper file1, FileWrapper file2)
        {
            FileInfo first = new FileInfo(Path.Combine(file1.Root, file1.Path));
            FileInfo second = new FileInfo(Path.Combine(file2.Root, file2.Path));
            if (first.Length != second.Length)
                return true;
            else
            {
                var firstSize = File.ReadAllBytes(first.FullName);
                var secondSize = File.ReadAllBytes(second.FullName);
                for (int i = 0; i < firstSize.Length; i++)
                {
                    if (firstSize[i] != secondSize[i])
                        return true;
                }
                return false;
            }
        }

        private static void CreateAllDirsForFile(string path, string root)
        {
            string current = root;
            string[] folders = path.Split(new char[] { '\\' });
            for (int i = 0; i < folders.Length - 1; i++)
            {
                current = Path.Combine(current, folders[i]);
                if (!Directory.Exists(current))
                    Directory.CreateDirectory(current);
            }
        }

        public static void Copy(Tuple<FileWrapper, string> replaceObj, ILogger logger)
        {
            CreateAllDirsForFile(replaceObj.Item2, replaceObj.Item1.Path);
            File.Copy(Path.Combine(replaceObj.Item1.Root, replaceObj.Item1.Path), Path.Combine(replaceObj.Item2, replaceObj.Item1.Path));
            logger.AddCopy(Path.Combine(replaceObj.Item1.Root, replaceObj.Item1.Path), Path.Combine(replaceObj.Item2, replaceObj.Item1.Path));
        }
    }
}
