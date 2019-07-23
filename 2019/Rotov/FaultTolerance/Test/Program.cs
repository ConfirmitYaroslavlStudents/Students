using System;
using System.IO;
using FaultTolerance;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            /*ToleranceLibrary.FallBack<IOException>(new Action(Read), new Action(Apologize));
            ToleranceLibrary.Retry<IOException>(new Action(Read), new Action(Apologize), 3);*/
        }


        public static void Read()
        {
            string data;
            using (StreamReader r = new StreamReader("input.txt"))
            {
                data = r.ReadToEnd();
            }
        }

        public static void Apologize()
        {
            Console.WriteLine("Sorry, but we don't write message");
        }
    }
}
