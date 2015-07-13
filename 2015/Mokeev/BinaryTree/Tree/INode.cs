using System;
using System.Collections.Generic;

namespace BinaryTree
{
	/// <summary>
	/// Provides base values for tree node
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface INode<T> : IEnumerable<INode<T>> where T : IComparable
	{
		/// <summary>
		/// Current deep level in tree
		/// </summary>
		ulong Level { get; set; }

		/// <summary>
		/// Data, saved in tree node
		/// </summary>
		T Data { get; }
		
		/// <summary>
		/// List of subtrees
		/// </summary>
		NodeList<T> SubTrees { get; }
	}
}