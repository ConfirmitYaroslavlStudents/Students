using System;

namespace FolderSynchronizerLib
{
    public class SyncException: Exception
    {
       public SyncException(string message): base(message) { }
    }
}
