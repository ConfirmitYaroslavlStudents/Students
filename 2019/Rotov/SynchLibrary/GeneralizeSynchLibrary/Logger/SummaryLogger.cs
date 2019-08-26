using System.Collections.Generic;
namespace GeneralizeSynchLibrary
{
    class SummaryLogger : ILogger
    {
        int _numOfReplace = 0;
        int _numOfRemove = 0;
        int _numOfCopy = 0;
        public List<string> GetLogs()
        {
            var result = new List<string>();
            if(_numOfReplace != 0)
                result.Add($"{_numOfReplace} were Replaced");
            if(_numOfRemove != 0)
                result.Add($"{_numOfRemove} were Removed");
            if (_numOfCopy != 0)
                result.Add($"{_numOfCopy} were Copy");
            return result;
        }

        public void AddReplace(params string[] payloads)
        {
            _numOfReplace++;
        }

        public void AddRemove(params string[] payloads)
        {
            _numOfRemove++;
        }

        public void AddCopy(params string[] payload)
        {
            _numOfCopy++;
        }
    }
}
