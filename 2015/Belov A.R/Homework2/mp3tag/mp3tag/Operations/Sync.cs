using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mp3TagLib;
using System.IO;

namespace mp3tager.Operations
{
    class Sync:Operation
    {
        public override void Call()
        {
            var path = Menu.GetUserInput("path:");
            var tager = new Tager(new FileLoader());
            var analyzer = new Analyzer(tager, s => Path.GetExtension(s).ToLower() == ".mp3");
            
            Menu.PrintHelp();
            
            var mask = new Mask(Menu.GetUserInput("mask:"));
            analyzer.Analyze(Directory.GetFiles(path), mask);

            var synchronizer = new Synchronizer(tager);
            synchronizer.Sync(analyzer.NotSynchronizedFiles, mask);
            
            Menu.PrintChanges(synchronizer.ModifiedFiles);
            if (Menu.GetUserInput("Enter 'save' to save changes\nEnter any string to ignore changes\n").ToLower() == "save")
            {
                synchronizer.Save();
                Menu.PrintCollection(string.Format("successfully with {0} errors", synchronizer.ErrorFiles.Count), synchronizer.ErrorFiles, ConsoleColor.Red);
                Menu.GetUserInput("Press enter...");
            }
        }
    }
}
