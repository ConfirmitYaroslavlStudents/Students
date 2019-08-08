using System;
using System.Collections.Generic;
using System.IO;

namespace FolderSynchronizerLib
{
    class InputDataReader
    {
        string _noDelete;
        string _loglevel;
        List<string> _validLogFlags;

        public InputDataReader()
        {
            _validLogFlags = new List<string>() { "verbose", "summary", "silent" };
            _noDelete = "--no-delete";
            _loglevel = "-loglevel";
        }

        public InputData Read(string[] args)
        {
            var input = new InputData();

            if(!IsInputValid(args))
            {
                throw new SyncException("Input is invalid");
            }

            input.MasterPath = args[0];
            input.SlavePath = args[1];
            var flagList = new List<string>();
            int count = 2;

            while (count < args.Length)
            {
                flagList.Add(args[count]);
                count++;
            }

            if (flagList.Contains(_noDelete))
            {
               input.NoDeleteFlag = true;
            }

            if (flagList.Contains(_loglevel))
            {
                input.LogFlag = GetLogFlag(flagList);
            }

            return input;
        }

        private string GetLogFlag(List<string> flagList)
        {
            string logFlag = "";

            try
            {
                logFlag = flagList[flagList.IndexOf(_loglevel) + 1];
            }
            catch (Exception)
            {
                throw new SyncException("Do not specify the type of logging");
            }

            if (!_validLogFlags.Contains(logFlag))
            {
                throw new SyncException("Invalid type of logging");
            }

            return logFlag;
        }

        public bool IsInputValid(string[] args)
        {
            if (args.Length < 2)
            {
                throw new SyncException("Invalid format command");
            }

            bool isMasterPathRight = Directory.Exists(args[0]);
            bool isSlavePathRight = Directory.Exists(args[1]);
           
            int count = 2;
            while (count < args.Length)
            {
                string word = args[count];
                if(!_validLogFlags.Contains(word) && _noDelete !=word && _loglevel != word)
                {
                    throw new SyncException("Invalid word: "+ word);
                }
            }

            return isMasterPathRight && isSlavePathRight;
        }
    }
}
