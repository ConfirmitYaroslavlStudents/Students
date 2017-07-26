using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MP3RenamerTimeMeasure
{
    public class MP3File
    {
        public string Path { get; private set; }
        public Permitions FilePermitions { get; set; }

        public MP3File(string path)
        {
            Path = path;
            FilePermitions = Permitions.Guest;
        }

        public MP3File(string path, Permitions permitions) : this(path)
        {
            FilePermitions = permitions;
        }

        public void Move(string newPath)
        {
            Path = newPath;
            Thread.Sleep((new Random()).Next(100));
        }
    }
}
