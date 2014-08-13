using System;

namespace Colors.Utils
{
    public class Processor
    {
        public void Process(Red red1, Red  red2)
        {
            Console.WriteLine("2 reds");
        }

        public void Process(Blue blue1, Blue blue2)
        {
            Console.WriteLine("2 blues");
        }

        public void Process(Blue blue)
        {
            Console.WriteLine("1 blue");
        }

        public void Process(Red red)
        {
            Console.WriteLine("1 red");
        }
    }
}
