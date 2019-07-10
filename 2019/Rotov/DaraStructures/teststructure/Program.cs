using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaraStructures;

namespace teststructure
{
    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();
            tree.Insert(new Node<int>(8, 1));
            tree.Insert(new Node<int>(3, 1));
            tree.Insert(new Node<int>(10, 1));
            tree.Insert(new Node<int>(1, 1));
            tree.Insert(new Node<int>(6, 1));
            tree.Insert(new Node<int>(14, 1));
            tree.Insert(new Node<int>(4, 1));
            tree.Insert(new Node<int>(7, 1));
            tree.Insert(new Node<int>(13, 1));
            tree.Remove(3);
        }
    }
}
