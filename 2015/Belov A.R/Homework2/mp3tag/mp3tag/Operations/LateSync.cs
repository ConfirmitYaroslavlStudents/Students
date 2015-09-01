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
        private Synchronizer _synchronizer;


        public LateSync()
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

            var path = Menu.GetUserInput("Load your plan\npath:");
            var planLoader = new PlanProvider(); 
            var tager = new Tager(new FileLoader());


            _synchronizer = new Synchronizer(tager);
            _synchronizer.Sync(planLoader.Load(path));
           
            
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
