using System;
using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public class InputDataReader
    {
        readonly string _noDelete;
        readonly string _loglevel;
        readonly List<string> _validLogFlags;
        private readonly IChecker _pathChecker;

        public InputDataReader(IChecker checker)
        {
            _validLogFlags = new List<string>() { "verbose", "summary", "silent" };
            _noDelete = "--no-delete";
            _loglevel = "-loglevel";
            _pathChecker = checker;
        }

        public InputData Read(string[] args)
        {
            var input = new InputData();

            if(!IsInputValid(args))
            {
                throw new SyncException("Input is invalid");
            }

            int count = 0;

            while (_pathChecker.IsValid(args[count]))
            {
                input.FoldersPaths.Add(args[count]);
                count++;
            }

            var flagList = new List<string>();

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
                input.LogLevel = GetLogFlag(flagList);
            }

            return input;
        }

        private LogLevels GetLogFlag(List<string> flagList)
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

            if (logFlag == "silent")
            {
                return LogLevels.silent;
            }

            if (logFlag == "verbose")
            {
                return LogLevels.verbose;
            }

            return LogLevels.summary;
        }

        public bool IsInputValid(string[] args)
        {
            if (args.Length < 2)
            {
                throw new SyncException("Invalid format command");
            }

            int count = 0;

            while (_pathChecker.IsValid(args[count]))
            {
                count++;
            }

            while (count < args.Length)
            {
                string word = args[count];
                if(!_validLogFlags.Contains(word) && _noDelete !=word && _loglevel != word)
                {
                    throw new SyncException("Invalid word: "+ word);
                }
                count++;
            }

            return true;
        }
    }
}
