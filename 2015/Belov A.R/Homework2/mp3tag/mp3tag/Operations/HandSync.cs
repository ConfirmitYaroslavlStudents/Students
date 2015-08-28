using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mp3TagLib;
using Mp3TagLib.Operations;
using Mp3TagLib.Sync;

namespace mp3tager.Operations
{
    class HandSync:Operation
    {
        public const int ID = 7;
        private Dictionary<IMp3File, Mp3Memento> _files;  
        public HandSync()
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

            var path = Menu.GetUserInput("path:");      //@"C:\Users\Alexandr\Desktop\TEST\New folder";
            var tager = new Tager(new FileLoader());
            var analyzer = new Analyzer(tager, s => Path.GetExtension(s).ToLower() == ".mp3");
            
            Menu.PrintHelp();
            
            var mask = new Mask(Menu.GetUserInput("mask:"));
           
            analyzer.Analyze(Directory.GetFiles(path), mask);
           
            var synchronizer = new Synchronizer(tager);
            
            foreach (var notSynchronizedFile in analyzer.NotSynchronizedFiles)
            {
                Console.Clear();
                Menu.PrintMessage(notSynchronizedFile.Key.Name+" with "+notSynchronizedFile.Value);
               
                var memento = notSynchronizedFile.Key.GetMemento();
               
                synchronizer.Sync(notSynchronizedFile.Key,mask,Menu.SelectSyncRule());
                Menu.PrintChanges(notSynchronizedFile.Key);
                
                if (Menu.GetYesNoAnswer("Save changes?\nY/N:"))
                {
                    _files.Add(notSynchronizedFile.Key, memento);
                    try
                    {
                        notSynchronizedFile.Key.Save();
                        Menu.PrintMessage("Successfully");
                        Menu.GetUserInput("Press enter...");
                    }
                    catch
                    {
                        Menu.PrintError("Can't save file");
                    }

                }
                else
                {
                    notSynchronizedFile.Key.SetMemento(memento);
                }

            }
        }

        public override void Cancel()
        {
            IsCanceled = true;
            RestoreFiles();
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
