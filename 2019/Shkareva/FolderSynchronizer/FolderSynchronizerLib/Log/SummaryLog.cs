using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSynchronizerLib
{
    class SummaryLog : ILog
    {
        private int _countOfAdd;
        private int _countOfUpdate;
        private int _countOfDelete;

        public string FormLogToPrint()
        {
            string log = "";

            log.Concat(_countOfAdd + " files have been added");
            log.Concat(Environment.NewLine);
            log.Concat(_countOfUpdate + " files have been updated");
            log.Concat(Environment.NewLine);
            log.Concat(_countOfDelete + " files have been deleted");

            return log;
        }

        public void GetInfoAboutAddFiles(string sourcePath, string destinationPath)
        {
            _countOfAdd++;
        }

        public void GetInfoAboutDeleteFiles(string sourcePath)
        {
            _countOfDelete++;
        }

        public void GetInfoAboutUpdateFiles(string sourcePath, string destinationPath)
        {
            _countOfUpdate++;
        }
    }
}
