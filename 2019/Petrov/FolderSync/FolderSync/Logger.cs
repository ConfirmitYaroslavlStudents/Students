using System;
using System.Collections.Generic;

namespace FolderSync
{
    class Logger
    {
        private int _numberOfDeletedFiles;
        private int _numberOfUpdatedFiles;
        private int _numberOfCopiedFiles;
        private List<string> _listOfChanges;

        public Logger()
        {
            _numberOfCopiedFiles = 0;
            _numberOfDeletedFiles = 0;
            _numberOfUpdatedFiles = 0;
            _listOfChanges = new List<string>();
        }

        public void PrintLog(string type)
        {
            if (type == "summary")
                PrintSummary();
            else
                PrintVerbose();
        }

        private void PrintVerbose()
        {
            foreach(var log in _listOfChanges)
                Console.WriteLine(log);
        }

        private void PrintSummary()
        {
            Console.WriteLine("deleted <{0}>",_numberOfDeletedFiles);
            Console.WriteLine("updated <{0}>", _numberOfUpdatedFiles);
            Console.WriteLine("copied <{0}>", _numberOfCopiedFiles);
        }

        public void IncreaseDeletedFilesCount()
        {
            ++_numberOfDeletedFiles;
        }

        public void IncreaseUpdatedFilesCount()
        {
            ++_numberOfUpdatedFiles;
        }

        public void IncreaseCopiedFilesCount()
        {
            ++_numberOfCopiedFiles;
        }

        public void AddToListOfChanges(string change)
        {
            _listOfChanges.Add(change);
        }
    }
}
