using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTree
{
	/// <summary>
	/// Node for tree
	/// </summary>
	/// <typeparam name="T">Type of objects, saved in tree</typeparam>
	public class TreeNode<T> : INode<T> where T : IComparable
	{
		/// <summary>
		/// Current level in tree
		/// </summary>
		public ulong Level { get; set; }
		
		/// <summary>
		/// Data to save in tree node
		/// </summary>
		public T Data { get; protected set; }

		/// <summary>
		/// List of subtrees
		/// </summary>
		public NodeList<T> SubTrees { get; set; }

		public TreeNode()
		{
			Level = 0;
			Data = default(T);
			SubTrees = null;
		}

		public TreeNode(T data, NodeList<T> subTrees = null)
		{
			Data = data;
			SubTrees = subTrees;
			Level = 0;
		}

		public IEnumerator<INode<T>> GetEnumerator()
		{
			// first of all return current node
			yield return this;

			// save all nodes we checking for subtrees in near future
			var nodesToCheck = new Queue<INode<T>>();

			if (SubTrees == null) yield break;

			foreach (var treeNode in SubTrees)
			{
				if(treeNode != null) nodesToCheck.Enqueue(treeNode);
			}

			while (nodesToCheck.Count != 0)
			{
				var currentElement = nodesToCheck.Dequeue();
				yield return currentElement;

				if (currentElement.SubTrees == null) continue;

				if (currentElement.SubTrees.Count == 0) continue;

				foreach (var node in currentElement.SubTrees.Where(treeNode => treeNode != null))
				{
					nodesToCheck.Enqueue(node);
				}

			}

			
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
