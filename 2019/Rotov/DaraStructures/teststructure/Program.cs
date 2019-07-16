using DaraStructures;
using System;

namespace teststructure
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new BinarySearchTree<int, int>(7, 9);
            tree.Insert(5, 10);
            tree.Insert(10, 15);
            foreach(var item in tree)
            {
                Console.WriteLine(item.Value);
            }
        }
    }
}
