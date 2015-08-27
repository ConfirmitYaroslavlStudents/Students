using System;
using System.Collections.Generic;
using System.IO;
using Mp3TagLib;
using Mp3TagLib.Operations;

namespace mp3tager.Operations
{
    class Changetags:Operation
    {
        public const int ID = 3;
        private Tager _tager;
        private Mp3Memento _memento;

        public Changetags()
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
          
            if (!_tager.Load(Menu.GetUserInput("path:")))
            {
                throw new FileNotFoundException("File does not exist");
            }
        
            Menu.PrintHelp();
            Menu.PrintCurrentFile(_tager.CurrentFile);
           
            var tags = GetTagsFromFileName(_tager.CurrentFile);
            
            _memento = _tager.CurrentFile.GetMemento();
            _tager.ChangeTags(tags);
            _tager.Save();
            
            Menu.PrintSuccessMessage();
        }
      
        Mp3Tags GetTagsFromFileName(IMp3File file)
        {
            if (file == null)
                throw new ArgumentException("File is not loaded");
            Menu.PrintHelp();
          
            var fileName = file.Name;
            var mask = new Mask(Menu.GetUserInput("mask:"));
            var tagValues = Select(mask.GetTagValuesFromString(fileName));
            var result = new Mp3Tags();
           
            foreach (var tagValue in tagValues)
            {
                result.SetTag(tagValue.Key, tagValue.Value);
            }
            return result;
        }
      
        Dictionary<string, string> Select(List<Dictionary<string, string>> tagValuesList)
        {
            Menu.PrintMessage("Select correct tag values:");
            Dictionary<string, string> result;
           
            var index = 0;
            do
            {
                Console.Clear();
                result = tagValuesList[index];
                Menu.PrintTagValues(result);
                Menu.PrintMessage(string.Format("{0}/{1}", index + 1, tagValuesList.Count) + "\n\t\tPress enter to select\n\t\tPress any key to scroll");
                if (++index >= tagValuesList.Count)
                    index = 0;

            } while (Console.ReadKey().Key != ConsoleKey.Enter);
           
            return result;
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
