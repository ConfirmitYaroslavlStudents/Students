using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MP3RenamerLib;

namespace MP3RenamerTimeMeasure
{
    public class Processor
    {
        public void Process(string[] args, MP3File[] files, Permitions userPermitions = Permitions.Guest)
        {
            if (files == null || files.Length == 0)
                throw new ArgumentException("No files to process");

            ArgumentsParser parser = new ArgumentsParser();
            Arguments arguments = parser.ParseArguments(args);

            foreach (var file in files)
            {
                string message = file.Path;
                IFileRenamer fileRenamer = new FileRenamer();

                if (arguments.IsCheckPermitions)
                {
                    IFileRenamer oldFileRenamer = fileRenamer;
                    IPermitionsChecker checker = new PermitionsChecker();
                    fileRenamer = new FileRenamerPermitionsChecker(oldFileRenamer, checker, userPermitions);
                }

                if (arguments.IsTimeMeasure)
                {
                    IFileRenamer oldFileRenamer = fileRenamer;
                    fileRenamer = new FileRenamerTimeMeasure(oldFileRenamer);
                }

                fileRenamer.Rename(file);

                message += " successfully renamed to " + file.Path;
                if (arguments.IsTimeMeasure)
                    message += " in " + ((FileRenamerTimeMeasure)fileRenamer).ElapsedTime;

                Console.WriteLine(message);
            }
        }
    }
}
