using System;
using System.Threading;

namespace TimeMeasurer
{
    public class Mp3File
    {
        public void MoveTo(string path)
        {
            Thread.Sleep(new Random().Next(50));
        }
    }
}