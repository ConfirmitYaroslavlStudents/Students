using System;
using System.Collections.Generic;
namespace DirectorySynchronizationApp
{
    public class Logger
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

        public int ShowNumberOfDeletedFiles()
        {
            return _numberOfDeletedFiles;
        }
        public int ShowNumberOfCopiedFiles()
        {
            return _numberOfCopiedFiles;
        }
        public int ShowNumberOfUpdatedFiles()
        {
            return _numberOfUpdatedFiles;
        }

        public void PrintLog(Enums.LogVariants type, Enums.WhereToPrint whereToPrint)
        {
            if (type == Enums.LogVariants.Summary)
                PrintSummary(whereToPrint);
            else
            if (type == Enums.LogVariants.Verbose)
                PrintVerbose(whereToPrint);
        }

        private void PrintVerbose(Enums.WhereToPrint whereToPrint)
        {
            if (whereToPrint == Enums.WhereToPrint.Console)
                PrintVerboseToConsole();
        }

        private void PrintVerboseToConsole()
        {
            foreach (var log in _listOfChanges)
                Console.WriteLine(log);
        }

        private void PrintSummary(Enums.WhereToPrint whereToPrint)
        {
            List<string> summaryList = MakeSummaryList();
            if (whereToPrint == Enums.WhereToPrint.Console)
                PrintSummaryToConsole(summaryList);
        }

        private List<string> MakeSummaryList()
        {
            List<string> summaryList = new List<string>();
            string tmp;
            tmp = "deleted <" + _numberOfDeletedFiles.ToString() + ">";
            summaryList.Add(tmp);
            tmp = "updated <" + _numberOfUpdatedFiles.ToString() + ">";
            summaryList.Add(tmp);
            tmp = "copied <" + _numberOfCopiedFiles.ToString() + ">";
            summaryList.Add(tmp);
            return summaryList;
        }

        private void PrintSummaryToConsole(List<string> summaryList)
        {
            foreach(var i in summaryList)
                Console.WriteLine(i);
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
