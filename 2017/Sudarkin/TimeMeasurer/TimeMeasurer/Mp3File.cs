using System;
using System.Threading;

namespace TimeMeasurer
{
    public class Mp3File
    {
        public string FullName { get; private set; }

        public Mp3File(string path = "DefaultPath")
        {
            FullName = path;
        }

        public void MoveTo(string path)
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next(50));

            FullName = path;
        }
    }
}