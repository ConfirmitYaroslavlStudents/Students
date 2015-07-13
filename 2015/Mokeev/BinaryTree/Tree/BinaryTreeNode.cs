using System;

namespace BinaryTree
{
	public class BinaryTreeNode<T> : TreeNode<T>, IBinaryNode<T> where T : IComparable
	{
		public BinaryTreeNode(T data) : base(data, null) { }
		public BinaryTreeNode(T data, BinaryTreeNode<T> left, BinaryTreeNode<T> right)
		{
			//save data to treenode
			Data = data;

			//create children list
			var children = new NodeList<T>(2);
			children[0] = left;
			children[1] = right;

			SubTrees = children;
		}

		/// <summary>
		/// Left branch
		/// </summary>
		public IBinaryNode<T> Left
		{
			get
			{
				if (SubTrees == null)
					return null;
				return (BinaryTreeNode<T>)SubTrees[0];
			}
			set
			{
				if (SubTrees == null)
					SubTrees = new NodeList<T>(2);

				SubTrees[0] = value;
			}
		}

		/// <summary>
		/// Right branch
		/// </summary>
		public IBinaryNode<T> Right
		{
			get
			{
				if (SubTrees == null)
					return null;
				return (BinaryTreeNode<T>)SubTrees[1];
			}
			set
			{
				if (SubTrees == null)
					SubTrees = new NodeList<T>(2);

				SubTrees[1] = value;
			}
		}

		/// <summary>
		/// Add new data item to the tree
		/// </summary>
		/// <param name="data"></param>
		public void Add(T data)
		{
			IBinaryNode<T> current = this;
			var level = 1UL;

			while (true)
			{
				//if there no more subtrees
				if (current.Left == null && current.Right == null)
				{
					// create new tree node with current deep level and place it in right position
					var treeData = new BinaryTreeNode<T>(data) {Level = level};
					if (data.CompareTo(current.Data) <= 0) current.Left = treeData;
					else current.Right = treeData;

					break;
				}
				else
				{

					if (data.CompareTo(current.Data) <= 0)
					{
						if (current.Left == null)
						{
							current.Left = new BinaryTreeNode<T>(data) {Level = level};
							break;
						}
						else
						{
							level++;
							current = current.Left;
						}
					}
					else
					{
						if (current.Right == null)
						{
							current.Right = new BinaryTreeNode<T>(data) { Level = level };
							break;
						}
						else
						{
							level++;
							current = current.Right;
						}
					}
				}
			}
		}

		public override string ToString()
		{
			return string.Format("{0}", Data);
		}
	}
}