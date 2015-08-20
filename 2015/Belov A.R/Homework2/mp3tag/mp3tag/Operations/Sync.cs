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
        private Dictionary<IMp3File, Mp3Memento> _files;  
        public Sync()
        {
            _files=new Dictionary<IMp3File, Mp3Memento>();
            OperationId = ID;
        }
        public override void Call()
        {
            if (IsCanceled)
            {
                RestoreFiles();
                return;
            }
           
            var path = @"C:\Users\Alexandr\Desktop\TEST";//Menu.GetUserInput("path:");
            var tager = new Tager(new FileLoader());
            var analyzer = new Analyzer(tager, s => Path.GetExtension(s).ToLower() == ".mp3");
            
            Menu.PrintHelp();
            
            var mask = new Mask(Menu.GetUserInput("mask:"));
            analyzer.Analyze(Directory.GetFiles(path), mask);

            var synchronizer = new Synchronizer(tager,Menu.SelectSyncRule());
            synchronizer.Sync(analyzer.NotSynchronizedFiles.Keys, mask);
            
            Menu.PrintChanges(synchronizer.ModifiedFiles);
            if (Menu.GetYesNoAnswer("Save changes?\nY/N:"))
            {
                SaveFilesState(synchronizer.ModifiedFiles);
                synchronizer.Save();
                Menu.PrintMessage("Successfully");
                Menu.PrintCollection(string.Format("with {0} errors", synchronizer.ErrorFiles.Count), synchronizer.ErrorFiles, ConsoleColor.Red);
                Menu.GetUserInput("Press enter...");
            }
        }

        public override void Cancel()
        {
            IsCanceled = true;
            RestoreFiles();
        }

        void SaveFilesState(IEnumerable<IMp3File> files)
        {
            foreach (var mp3File in files)
            {
                _files.Add(mp3File, mp3File.GetMemento());
            }
        }

        void RestoreFiles()
        {
            for (int i = 0; i < _files.Count; i++)
            {
                var file = _files.Keys.ElementAt(i);
                var newMemento = file.GetMemento();
                file.SetMemento(_files[file]);
                _files[file] = newMemento;
                file.Save();
            }
        }
    }
}
