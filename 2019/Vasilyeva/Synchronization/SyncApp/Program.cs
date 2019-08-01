using SyncLib;

namespace SyncApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Synchronization synchronization = new Synchronization(@"C:\Users\stalk\Desktop\vs\Synchronization\SyncLib\bin\Debug\master", @"C:\Users\stalk\Desktop\vs\Synchronization\SyncLib\bin\Debug\slave");
            synchronization.Synchronaze();
        }
    }
}
