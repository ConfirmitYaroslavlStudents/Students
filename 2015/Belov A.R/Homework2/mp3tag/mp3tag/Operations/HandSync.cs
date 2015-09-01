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
        private Synchronizer synchronizer;

        public HandSync()
        {
            OperationId = ID;
        }
    
        public override void Call()
        {
            if (IsCanceled)
            {

                synchronizer.Redo();
                return;
            }

            var path = Menu.GetUserInput("path:");      //@"C:\Users\Alexandr\Desktop\TEST\New folder";
            var tager = new Tager(new FileLoader());
            var analyzer = new Analyzer(tager, s => Path.GetExtension(s).ToLower() == ".mp3");


            Menu.PrintHelp();



            var mask = new Mask(Menu.GetUserInput("mask:"));



            analyzer.Analyze(Directory.GetFiles(path), mask);



            synchronizer = new Synchronizer(tager);
            
            foreach (var notSynchronizedFile in analyzer.NotSynchronizedFiles)
            {
                Console.Clear();
                Menu.PrintMessage(notSynchronizedFile.Key.Name+" with "+notSynchronizedFile.Value);
               

               
                synchronizer.Sync(notSynchronizedFile.Key,mask,Menu.SelectSyncRule());
                Menu.PrintChanges(notSynchronizedFile.Key);



                if (Menu.GetYesNoAnswer("Save changes?\nY/N:"))
                {
                    try
                    {
                        synchronizer.SaveLast();
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
                    synchronizer.RestoreLast();
                }
            }
        }
    
        public override void Cancel()
        {
            IsCanceled = true;
            synchronizer.Restore();
        }

    }
}
