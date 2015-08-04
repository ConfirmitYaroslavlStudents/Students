using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mp3TagLib;

namespace mp3tager
{
    class Analysis:Operation
    {
        public override void Call()
        {
            //[TODO] SRP vialation
            //[TODO] need tests
            Menu.PrintHelp();
            var path = @"C:\Users\Alexandr\Desktop\TEST";// Menu.GetUserInput("path:");
            var analyzer = new Analyzer(new Tager(new FileLoader()));
            var mask = new Mask(Menu.GetUserInput("mask:"));
            analyzer.Analyze(Directory.GetFiles(path), mask);
            if (!analyzer.NotSynchronizedFiles.Any()&&!analyzer.ErrorFiles.Keys.Any())
            {
                Menu.PrintMessage("All files is OK");
                Menu.GetUserInput("Press enter...");
                return;
            }
            if (analyzer.ErrorFiles.Keys.Any())
            Menu.PrintCollection(
                       string.Format("{0} files can't be loaded", analyzer.ErrorFiles.Count()),
                       analyzer.ErrorFiles,ConsoleColor.Red);
            Menu.PrintCollection("Not synchronized files:", (from file in analyzer.NotSynchronizedFiles select file.Name + ".mp3"), ConsoleColor.Red);
            Menu.PrintCollection("Synchronized files:", (from file in analyzer.SynchronizedFiles select file.Name+".mp3"), ConsoleColor.Green);
            Menu.GetUserInput("Press enter...");
        }
    }
}
