using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mp3TagLib;
using Mp3TagLib.Operations;
using Mp3TagLib.Plan;
using Mp3TagLib.Sync;

namespace mp3tager.Operations
{
    class LateSync:Operation
    {
        public const int ID = 8;
        private Dictionary<IMp3File, Mp3Memento> _files;
        public LateSync()
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

            var path = Menu.GetUserInput("Load your plan\npath:");
            var planLoader = new PlanProvider(); 
            var tager = new Tager(new FileLoader());
            var synchronizer = new Synchronizer(tager);

           
            synchronizer.Sync(planLoader.Load(path));
           
            
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
