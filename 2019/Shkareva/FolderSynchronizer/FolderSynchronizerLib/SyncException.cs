using System;

namespace FolderSynchronizer
{
    public class SyncException: Exception
    {
       public SyncException(string message): base(message) { }
    }
}
