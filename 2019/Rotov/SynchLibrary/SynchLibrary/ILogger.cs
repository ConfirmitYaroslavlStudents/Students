using System.Collections.Generic;
namespace SynchLibrary
{
    public interface ILogger
    {
        List<string> GetLogs();

        void AddReplace(params string[] payload);

        void AddRemove(params string[] payload);

        void AddCopy(params string[] payload);
    }
}
