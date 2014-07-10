using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forest;

namespace Wooden_application_console_
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree<int> birch;

            //birch = new Tree<int>(new int[] { 10, 8, 7, 15, 9, 7 });//Test1

            //birch = new Tree<int>();//Test2
            //birch.AddRange(new int[] { 10, 8, 7, 15, 9 });

            //birch = new Tree<int>();//Test3
            //birch.Add(15);
            //birch.Add(8);
            //birch.Add(9);
            //birch.Add(7);
            //birch.Add(10);

            //birch = new Tree<int>(new int[] { 10 });//Test4
            //birch.Remove(10);

            birch = new Tree<int>(new int[] { 10, 8, 7, 15, 9 });//Test4
            birch.Remove(7);

            //birch = new Tree<int>(new int[] { 30, 40, 20, 25, 26, 10, 6, 3 });//Test5
            //birch.Remove(20);

            Console.ReadLine();
        }
    }
}
