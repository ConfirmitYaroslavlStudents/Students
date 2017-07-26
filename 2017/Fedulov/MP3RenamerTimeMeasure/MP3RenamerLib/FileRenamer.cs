using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MP3RenamerTimeMeasure;

namespace MP3RenamerLib
{
    public interface IFileRenamer
    {
        void Rename(MP3File file);
    }

    public class FileRenamer : IFileRenamer
    {
        public void Rename(MP3File file)
        {
            string[] splittedPath = file.Path.Split('.');
            string newPath = splittedPath[0] + "_(new)." + splittedPath[1];
            file.Move(newPath);
        }
    }

    public class FileRenamerPermitionsChecker : IFileRenamer
    {
        private IFileRenamer fileRenamer;
        private IPermitionsChecker permitionsChecker;
        private Permitions userPermitions;

        public FileRenamerPermitionsChecker(IFileRenamer fileRenamer, 
            IPermitionsChecker permitionsChecker, Permitions userPermitions = Permitions.Guest)
        {
            this.fileRenamer = fileRenamer;
            this.permitionsChecker = permitionsChecker;
            this.userPermitions = userPermitions;
        }

        public void Rename(MP3File file)
        {
            if (permitionsChecker.Check(file, userPermitions))
                fileRenamer.Rename(file);
        }
    }

    public class FileRenamerTimeMeasure : IFileRenamer
    {
        private IFileRenamer fileRenamer;
        private Stopwatch stopwatch;

        public TimeSpan ElapsedTime { get; private set; }

        public FileRenamerTimeMeasure(IFileRenamer fileRenamer)
        {
            this.fileRenamer = fileRenamer;
            stopwatch = new Stopwatch();
        }

        public void Rename(MP3File file)
        {
            stopwatch.Start();
            fileRenamer.Rename(file);
            stopwatch.Stop();

            ElapsedTime += stopwatch.Elapsed;
            stopwatch.Reset();
        }
    }
}
