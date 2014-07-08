using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tree;
using Queue;

namespace BreadthFirstRound
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new Tree<int>();
            tree.Add(5);
            tree.Add(2);
            tree.Add(45);
            tree.Add(1);
            tree.Add(3);
            tree.Add(13);
            tree.Add(41);
            tree.Add(70);
            tree.Add(60);
            tree.Add(80);
            var BFS = new BreadthFirstRound<int>(tree, 3);
            BFS.EventAfterGettingIntoNewNode += PrintCurrentNode;
            BFS.Start();
        }
        static void PrintCurrentNode(int node)
        {
            Console.WriteLine("Cейчас я в вершине: " + node);
            Console.ReadKey();
        }
    }
}
