using System;

namespace BinaryTree
{
	/// <summary>
	/// Interface of binary node, that provides Right and Left branches for the tree
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IBinaryNode<T> : INode<T> where T : IComparable
	{
		IBinaryNode<T> Right { get; set; }
		IBinaryNode<T> Left { get; set; }
	}
}