using System.Collections.Generic;

namespace FileLib
{
    // todo: *done* too many file system abstractions
    public abstract class BaseDirectory
    {
        public abstract IEnumerable<IMp3File> GetFiles(string directory);

        protected abstract bool Exists(string path);

        public string CreateUniqueName(string path)
        {
            var directory = System.IO.Path.GetDirectoryName(path);
            var fileName = System.IO.Path.GetFileNameWithoutExtension(path);
            var destinationPath = System.IO.Path.Combine(directory, fileName + @".mp3");

            var index = 1;

            while (Exists(destinationPath))
            {
                destinationPath = System.IO.Path.Combine(directory, fileName + @" (" + index + ").mp3");
                index++;
            }

            return destinationPath;
        }
    }
}
