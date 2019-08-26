using System.Collections.Generic;
namespace GeneralizeSynchLibrary
{
    public interface ILogger
    {
        List<string> GetLogs();

        void AddReplace(params string[] payload);

        void AddRemove(params string[] payload);

        void AddCopy(params string[] payload);
    }
}
