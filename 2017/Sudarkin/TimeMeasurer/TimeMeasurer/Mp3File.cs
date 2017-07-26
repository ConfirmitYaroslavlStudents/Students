using System;
using System.Threading;

namespace TimeMeasurer
{
    public class Mp3File
    {
        public void MoveTo(string path)
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next(50));
        }
    }
}