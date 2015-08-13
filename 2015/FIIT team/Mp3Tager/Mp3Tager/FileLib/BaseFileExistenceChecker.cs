using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLib
{
    public abstract class BaseFileExistenceChecker
    {
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
