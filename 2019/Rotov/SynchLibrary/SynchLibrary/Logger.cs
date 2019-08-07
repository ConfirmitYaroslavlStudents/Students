using System;

namespace SynchLibrary
{
    public enum TypesOfLogging : byte
    {
        Silent,
        Summary,
        Verbose
    }

    class Logger
    {
        TypesOfLogging _type;

        int _numOfReplace = 0;
        int _numOfRemove = 0;
        int _numOfCopy = 0;

        public Logger(TypesOfLogging type)
        {
            _type = type;
        }

        public void PrintLogs()
        {
            if (_type == TypesOfLogging.Summary)
            {
                PrintSummary();
            }
            else if (_type == TypesOfLogging.Verbose)
            {
                PrintVerbose();
            }
        }

        public void AddReplace()
        {
            _numOfReplace++;
        }

        public void AddRemove()
        {
            _numOfReplace++;
        }

        public void AddCopy()
        {
            _numOfCopy++;
        }

        private void PrintVerbose()
        {
            throw new NotImplementedException();
        }

        private void PrintSummary()
        {
            if(_numOfCopy != 0)
                Console.WriteLine($"{_numOfCopy} were Copied");
            if(_numOfReplace != 0)
                Console.WriteLine($"{_numOfReplace} were Replaced");
            if(_numOfRemove != 0)
                Console.WriteLine($"{_numOfRemove} were Removed");
        }
    }
}
