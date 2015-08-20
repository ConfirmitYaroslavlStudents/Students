using System.IO;
using Mp3TagLib;
using Mp3TagLib.Operations;

namespace mp3tager.Operations
{
    class Rename:Operation
    {
        public const int ID = 4;
        private Tager _tager;
        private Mp3Memento _memento;

        public Rename()
        {
            OperationId = ID;
        }
        public override void Call()
        {
            if (IsCanceled)
            {
                RestoreFile();
                return;
            }
        
            _tager = new Tager(new FileLoader());
                                                           // _tager.Load(@"C:\Users\Alexandr\Desktop\TEST\песня.mp3");
            if (!_tager.Load(Menu.GetUserInput("path:")))
            {
                throw new FileNotFoundException("File does not exist");
            }
          
            Menu.PrintHelp();
            _memento=_tager.CurrentFile.GetMemento();
            _tager.ChangeName(new Mask(Menu.GetUserInput("mask:")));
            _tager.Save();
            Menu.PrintSuccessMessage();
        }

        public override void Cancel()
        {
            RestoreFile();
            IsCanceled = true;
        }
        void RestoreFile()
        {
            var newMemento = _tager.CurrentFile.GetMemento();
            _tager.CurrentFile.SetMemento(_memento);
            _memento = newMemento;
            _tager.Save();
        }
    }
}
