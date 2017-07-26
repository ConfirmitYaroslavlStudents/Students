using System.Collections.Generic;

namespace TaggerLib
{
    internal class Dir 
    {
        private static string[] GetPathsFiles(InputData inputData)
        {
            string[] pathsfiles;
            if (inputData.Subfolders)
                pathsfiles = System.IO.Directory.GetFiles(inputData.Path, inputData.Mask, System.IO.SearchOption.AllDirectories);
            else
                pathsfiles = System.IO.Directory.GetFiles(inputData.Path, inputData.Mask);

            return pathsfiles;
        }

        public static List<File> GetFiles(InputData inputData)
        {
            var files = new List<File>();
            var pathsfiles = GetPathsFiles(inputData);
            foreach (var path in pathsfiles)
            {
                var file = new File(path);
                file.GetTags();
                files.Add(file);
            }

            return files;
        }       
    }
}
