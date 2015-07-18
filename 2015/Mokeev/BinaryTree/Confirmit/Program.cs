using System;
using System.Text;
using BinaryTree;

namespace Program
{
	static class Program
	{
		static void Main()
		{
			var binaryTree = new BinaryTree<int> { 90, 50, 150, 20, 75, 5, 66, 80, 68 };

			Console.WriteLine("Created tree:");
			PrintTree(binaryTree);

			var nodeToRemove = binaryTree[(new Random()).Next(0, binaryTree.Count)];
			binaryTree.Remove(nodeToRemove);

			Console.WriteLine("\nTree with removed node [ {0} ]:", nodeToRemove);
			PrintTree(binaryTree);

			Console.WriteLine("\n5th element is: {0}", binaryTree[4]);

			// creating more interesting tree
			var tree = new BinaryTree<int> { 90, 50, 150, 20, 75, 95, 175, 5, 25, 66, 80, 92, 111, 166, 200 };

			Console.WriteLine("\n\nPreorder traverse:");
			tree.Traverse(node => Console.Write("[{1}]:{0} ", node, node.Level), "Preorder");

			Console.WriteLine("\n\nInorder Traverse:");
			tree.Traverse(node => Console.Write("[{1}]:{0} ", node, node.Level), "Inorder");

			Console.WriteLine("\n\nPostorder Traverse:");
			tree.Traverse(node => Console.Write("[{1}]:{0} ", node, node.Level), "Postorder");

			Console.WriteLine("\n\nWidth Traverse (foreach use this method):");
			tree.Traverse(node => Console.Write("[{1}]:{0} ", node, node.Level), "Width");


			Console.WriteLine();

			Console.ReadKey();
		}

		static void PrintTree(BinaryTree<int> bt)
		{
			foreach (var node in bt)
			{
				var sb = new StringBuilder(".");
				for (var i = 0UL; i < node.Level; i++)
				{
					sb.Append(".");
				}

				Console.WriteLine(sb.ToString() + node.Data);
			}
		}
	}
}
