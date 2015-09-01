using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mp3TagLib;
using Mp3TagLib.Operations;
using Mp3TagLib.Sync;

namespace mp3tager.Operations
{
    class Sync:Operation
    {
        public const int ID = 6;

        private Synchronizer _synchronizer;



        public Sync()
        {

            OperationId = ID;
        }
      
        public override void Call()
        {
            if (IsCanceled)
            {
                _synchronizer.Redo();
                return;
            }



            var path = Menu.GetUserInput("path:"); //@"C:\Users\Alexandr\Desktop\TEST";
            var tager = new Tager(new FileLoader());
            var analyzer = new Analyzer(tager, s => Path.GetExtension(s).ToLower() == ".mp3");



            Menu.PrintHelp();



            var mask = new Mask(Menu.GetUserInput("mask:"));



            analyzer.Analyze(Directory.GetFiles(path), mask);


            _synchronizer = new Synchronizer(tager,Menu.SelectSyncRule());
            _synchronizer.Sync(analyzer.NotSynchronizedFiles.Keys, mask);



            Menu.PrintChanges(_synchronizer.ModifiedFiles);
            if (Menu.GetYesNoAnswer("Save changes?\nY/N:"))
            {
                _synchronizer.Save();
                Menu.PrintMessage("Successfully");
                Menu.PrintCollection(string.Format("with {0} errors", _synchronizer.ErrorFiles.Count), _synchronizer.ErrorFiles, ConsoleColor.Red);
                Menu.GetUserInput("Press enter...");
            }
        }

        public override void Cancel()
        {
            IsCanceled = true;
            _synchronizer.Restore();
        }
    }
}
