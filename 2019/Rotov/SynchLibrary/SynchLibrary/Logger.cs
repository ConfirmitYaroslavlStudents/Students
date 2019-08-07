using System;
using System.Collections.Generic;
namespace SynchLibrary
{
    public enum TypesOfLogging : int
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

        List<string> _output;

        public Logger(TypesOfLogging type)
        {
            _type = type;
            if (type == TypesOfLogging.Verbose)
                _output = new List<string>();
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

        public void AddReplace(string filename, string root)
        {
            if (_type == TypesOfLogging.Verbose)
                _output.Add($"{filename} replaced to {root}");
            _numOfReplace++;
        }

        public void AddRemove(string filename, string root)
        {
            if (_type == TypesOfLogging.Verbose)
                _output.Add($"{filename} removed form {root}");
            _numOfRemove++;
        }

        public void AddCopy(string filename, string input, string output)
        {
            if (_type == TypesOfLogging.Verbose)
                _output.Add($"{filename} copied from {input} to {output}");
            _numOfCopy++;
        }

        private void PrintVerbose()
        {
            foreach (var line in _output)
                Console.WriteLine(line);
        }

        private void PrintSummary()
        {
            if (_numOfCopy != 0)
                Console.WriteLine($"{_numOfCopy} were Copied");
            if (_numOfReplace != 0)
                Console.WriteLine($"{_numOfReplace} were Replaced");
            if (_numOfRemove != 0)
                Console.WriteLine($"{_numOfRemove} were Removed");
        }
    }
}
