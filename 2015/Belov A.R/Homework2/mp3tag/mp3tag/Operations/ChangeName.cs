using System.IO;
using Mp3TagLib;
using Mp3TagLib.Operations;
using Mp3TagLib.Sync;

namespace mp3tager.Operations
{
    class ChangeName:Operation
    {
        public const int ID = 4;
        private Mp3TagLib.Sync.Rename rename;


        public ChangeName()
        {
            OperationId = ID;
        }
     
        public override void Call()
        {
            if (IsCanceled)
            {
                rename.Redo();
                return;
            }
        
            var tager = new Tager(new FileLoader());
                                                           // _tager.Load(@"C:\Users\Alexandr\Desktop\TEST\песня.mp3");
            if (!tager.Load(Menu.GetUserInput("path:")))
            {
                throw new FileNotFoundException("File does not exist");
            }




            Menu.PrintHelp();






            rename = new Rename();
            rename.Call(new Mask(Menu.GetUserInput("mask:")), tager,tager.CurrentFile);
            rename.Save();



            Menu.PrintSuccessMessage();
        }

        public override void Cancel()
        {
            rename.Cancel();
            rename.Save();
            IsCanceled = true;
        }
       
    }
}
