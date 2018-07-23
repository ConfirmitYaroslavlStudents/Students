using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using StackClass;

namespace MyStack
{
    class Program
    {
        static void Main()
        {
            string[] text;
            using (StreamReader sr = new StreamReader("C:/Users/ZooM/Desktop/Документы/WarAndWorld.txt"))
            {
                text = sr.ReadToEnd().Split(new[] {' ', '.', '!', '?', '/','\"','\'','(',')',':',',',';'});
            }
            var mySt = new MyStack<string>(text.Length);
            Stopwatch timer = new Stopwatch();

            timer.Start();
            foreach (var word in text)
            {
                mySt.Push(word);
            }
            timer.Stop();

            Console.WriteLine("Count: {0}", text.Length);
            Console.WriteLine();
            Console.WriteLine("Time push: {0}", timer.ElapsedMilliseconds);

            //timer.Start();
            //foreach (var word in text)
            //{
            //    mySt.Contains(word);
            //}
            //timer.Stop();

            //Console.WriteLine();
            //Console.WriteLine("Time contains All Elements: {0}", timer.ElapsedMilliseconds);

            timer.Start();
            foreach (var word in text)
            {
                mySt.Pop();
            }
            timer.Stop();

            Console.WriteLine();
            Console.WriteLine("Time pop All Elements: {0}", timer.ElapsedMilliseconds);

            Console.ReadKey();
        }
    }
}
