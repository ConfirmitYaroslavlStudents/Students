using System.Collections.Generic;

namespace GeneralizeSynchLibrary
{
    public class SilentLogger : ILogger
    {
        public void AddCopy(params string[] payload)
        {
            throw new System.NotImplementedException();
        }

        public void AddRemove(params string[] payload)
        {
            return;
        }

        public void AddReplace(params string[] payload)
        {
            return;
        }

        public List<string> GetLogs()
        {
            return new List<string>();
        }
    }
}
