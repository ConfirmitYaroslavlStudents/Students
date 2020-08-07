using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> MyQueue = new Queue<int>();
            System.Collections.Generic.Queue<int> MicrosoftStack = new System.Collections.Generic.Queue<int>();

            var startTime = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < 100000; i++)
                MyQueue.Enqueue(i);
            MyQueue.Contains(3);
            for (int i = 0; i < 100000; i++)
                MyQueue.Dequeue();

            startTime.Stop();
            var resultTime = startTime.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                resultTime.Hours,
                resultTime.Minutes,
                resultTime.Seconds,
                resultTime.Milliseconds);

            Console.WriteLine("Time Work My Queue {0}", elapsedTime);




            startTime = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < 100000; i++)
                MicrosoftStack.Enqueue(i);
            MicrosoftStack.Contains(3);
            MicrosoftStack.Peek();
            for (int i = 0; i < 100000; i++)
                MicrosoftStack.Dequeue();

            startTime.Stop();
            resultTime = startTime.Elapsed;
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                resultTime.Hours,
                resultTime.Minutes,
                resultTime.Seconds,
                resultTime.Milliseconds);

            Console.WriteLine("Time Work Microsoft Queue {0}", elapsedTime);

        }
    }
}
