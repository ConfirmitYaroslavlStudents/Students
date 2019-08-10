using System.Collections.Generic;

namespace SynchLibrary
{
    public interface ISynchronizer
    {
        ILogger Synchronize(string master , string slave , ILogger logger);
    }
}
