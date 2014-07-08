using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> myQueue = Queue<int>.GetQueue();
            Queue<int> myQueue2 = Queue<int>.GetQueue();
            if (myQueue == myQueue2)
                Console.WriteLine("тождество");
            myQueue.Enqueue(1);
            Console.WriteLine(myQueue.Dequeue());
            myQueue.Enqueue(3);
            myQueue.Enqueue(2);
            myQueue.Enqueue(1);
            Console.WriteLine(myQueue.Peek());
            Console.WriteLine("Count=" + myQueue.Count);
            int[] exampleArray = new int[myQueue.Count];
            exampleArray = myQueue.ToArray();
            for (int i = 0; i < myQueue.Count; i++)
            {
                Console.WriteLine(exampleArray[i]);
            }
            foreach (int iteam in myQueue)
            {
                Console.Write(iteam + " ");
            }
            Console.ReadLine();
        }
    }
}