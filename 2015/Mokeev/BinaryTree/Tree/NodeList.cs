using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTree
{
	/// <summary>
	/// Special list for tree nodes
	/// </summary>
	/// <typeparam name="T">Data type</typeparam>
	public class NodeList<T> : List<INode<T>> where T : IComparable
	{
		public NodeList(int capacity) : base(capacity)
		{
			for (var i = 0; i < capacity; i++)
			{
				Add(default(TreeNode<T>));
			}
		}
	}
}