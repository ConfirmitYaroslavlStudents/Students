using System;
using System.Diagnostics;
using Forest;

namespace WoodenApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var birch = new Tree<int>(new int[] { 50, 25, 75, 12, 38, 70, 80, 6, 13, 37, 39, 67, 71, 79, 81 });
            var action1 = new Action<Node<int>>((Node<int> nx) => Trace.Write(string.Format("{0} ", nx.Value)));
            birch.Horizontal(action1);
        }
    }
}
