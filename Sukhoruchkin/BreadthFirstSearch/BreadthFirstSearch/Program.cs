using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tree;
using Queue;

namespace BreadthFirstSearch
{
    class Program
    {

        static void Main(string[] args)
        {
            Tree.Tree<int> tree = new Tree<int>();
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
            Program prg = new Program();
            prg.BreadthFirstSearch(tree,12);
            Console.ReadLine();

        }
        void BreadthFirstSearch(Tree<int> tree, int node)
        {
            var queue = new Queue<Tree.Node<int>>();
            var parentQueue = new Queue<Tree.Node<int>>();
            var nodeInGraph = tree.SearchElement(node,tree._root);
            if(nodeInGraph==null)
                throw new InvalidOperationException("This element is not present in the tree.");
            Tree.Node<int> parentNode=null;
            bool isHaveChildren = false;
            queue.Enqueue(nodeInGraph);
            parentNode = nodeInGraph;
            while (queue.Count > 0)
            {
                nodeInGraph = queue.Dequeue();
                if ((nodeInGraph.Left != parentNode) && (nodeInGraph.Parent != parentNode) && (nodeInGraph.Right != parentNode))
                {
                    try { parentNode = parentQueue.Dequeue(); }
                    catch { }
                }
                if ((nodeInGraph.Parent != null) && (nodeInGraph.Parent != parentNode)) 
                {
                    queue.Enqueue(nodeInGraph.Parent);
                    isHaveChildren = true;
                }
                if ((nodeInGraph.Left != null) && (nodeInGraph.Left != parentNode)) 
                {
                    queue.Enqueue(nodeInGraph.Left);
                    isHaveChildren = true;
                }
                if ((nodeInGraph.Right != null) && (nodeInGraph.Right != parentNode)) 
                {
                    queue.Enqueue(nodeInGraph.Right);
                    isHaveChildren = true;
                }
                if((parentNode!=nodeInGraph)&&(isHaveChildren))
                parentQueue.Enqueue(nodeInGraph);
                isHaveChildren = false;
            }
        }
    }
}
