using System.Collections.Generic;
namespace GeneralizeSynchLibrary
{
    public class VerboseLogger : ILogger
    {
        List<string> _output = new List<string>();
        public List<string> GetLogs()
        {
            return _output;
        }

        public void AddReplace(params string[] payload)
        {
            _output.Add($"{payload[0]} replaced to {payload[1]}");
        }

        public void AddRemove(params string[] payload)
        {
            _output.Add($"{payload[0]} removed");
        }
    }
}
