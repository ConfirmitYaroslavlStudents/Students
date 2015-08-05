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
            var sync=new Synchronizer(tager);
            sync.Sync(analyzer.NotSynchronizedFiles,mask);
            Menu.PrintChanges(sync.ModifiedFiles);
            if (Menu.GetUserInput("Enter 'save' to save changes\nEnter any string to ignore changes\n").ToLower() == "save")
            {
                sync.Save();
                Menu.PrintCollection(string.Format("successfully with {0} errors", sync.ErrorFiles.Count), sync.ErrorFiles, ConsoleColor.Red);
                Menu.GetUserInput("Press enter...");
            }
        }
    }
}
