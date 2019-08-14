using FolderSynchronizerLib;

namespace FolderSynchronizer
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new InputDataReader(new FolderPathChecker()).Read(args);
            new Launcher().Synchronize(input);           
        }       
    }
}
