using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderSynchronizerLib
{
    class VerboseLog : ILog
    {
        List<(string,string)> _addInfoList = new List<(string, string)>();
        List<(string, string)> _updateInfoList = new List<(string, string)>();
        List<string> _deleteInfoList = new List<string>();

        public string FormLogToPrint()
        {
            string log = "";

            foreach(var pair in _addInfoList)
            {
                log.Concat("File " + pair.Item1 + " has been copied to " + Path.GetDirectoryName(pair.Item2));
                log.Concat(Environment.NewLine);
            }

            foreach (var pair in _updateInfoList)
            {
                log.Concat("File " + pair.Item2 + " has been updated to " + pair.Item1);
                log.Concat(Environment.NewLine);
            }

            foreach (var file in _deleteInfoList)
            {
                log.Concat("File " + file + " has been deleted");
                log.Concat(Environment.NewLine);
            }

            return log;
        }

        public void GetInfoAboutAddFiles(string sourcePath, string destinationPath)
        {
            _addInfoList.Add((sourcePath, destinationPath));
        }

        public void GetInfoAboutDeleteFiles(string sourcePath)
        {
            _deleteInfoList.Add(sourcePath);
        }

        public void GetInfoAboutUpdateFiles(string sourcePath, string destinationPath)
        {
            _updateInfoList.Add((sourcePath, destinationPath));
        }
    }
}
