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
            Queue<int> queue1 = new Queue<int>();
            Console.WriteLine(queue1.Count);

            //Console.WriteLine(queue1.Dequeue());
            queue1.Enqueue(1);
            queue1.Enqueue(2);
            queue1.Enqueue(3);
            queue1.Enqueue(4);
            queue1.Enqueue(5);
            Console.WriteLine(queue1.Count);
            Console.WriteLine(queue1.Dequeue());
            queue1.Enqueue(6);
            queue1.Enqueue(7);
            Console.WriteLine(queue1.Count);
            Console.WriteLine(queue1.Peek());
            Console.WriteLine(queue1.Count);
        }
    }
}
