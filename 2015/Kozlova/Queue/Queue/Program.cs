using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<string> words = new Queue<string>();
            words.Enqueue("one");
            words.Enqueue("two");
            words.Enqueue("three");
            words.Enqueue("four");
            words.Enqueue("five");
            words.Enqueue("six");
            words.Dequeue();

            foreach (string number in words)
            {
                Console.WriteLine(number);
            }
            words.Print();

            Queue<int> numbers = new Queue<int>(5);
            Queue<int> numbers2 = new Queue<int>(5);
            QueueFromList<int> numbers3 = new QueueFromList<int>(5);
            QueueFromList<int> numbers4 = new QueueFromList<int>(6);
            Console.WriteLine((numbers == numbers2).ToString());
            Console.WriteLine((numbers3 == numbers4).ToString());

            QueueFromList<int> numbers5 = new QueueFromList<int>(5, 6, 7);
            Console.WriteLine(numbers5.ToString());
        }
    }
}
