using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTree
{
	/// <summary>
	/// Implementation of binary tree
	/// </summary>
	/// <typeparam name="T">Data type for this binary tree</typeparam>
	public sealed class BinaryTree<T> : ICollection<INode<T>>, ICloneable where T : IComparable
	{
		public delegate void Traversing(IBinaryNode<T> node);

		public event Traversing OnTraverse;

		private BinaryTreeNode<T> RootNode { get; set; }

		/// <summary>
		/// Amount of elements in this tree
		/// </summary>
		public int Count { get; private set; }
		public bool IsReadOnly { get { return false; } }

		/// <summary>
		/// Creates Binary tree
		/// </summary>
		public BinaryTree()
		{
			RootNode = null;
		}

		/// <summary>
		/// Add data to the binary tree
		/// </summary>
		/// <param name="item">Item with data to be added in tree</param>
		public void Add(INode<T> item)
		{
			// if there is no object then exit
			if (item == null) return;

			Add(item.Data);
		}

		/// <summary>
		/// Add data to the binary tree
		/// </summary>
		/// <param name="data">Data to be added in tree</param>
		public void Add(T data)
		{
			Count++;

			// if there is no elements in tree
			if (RootNode == null)
			{
				RootNode = new BinaryTreeNode<T>(data);
				return;
			}

			RootNode.Add(data);
		}

		/// <summary>
		/// Clear this  tree
		/// </summary>
		public void Clear()
		{
			RootNode = null;
			Count = 0;
		}

		/// <summary>
		/// Copy elements from tree to array
		/// </summary>
		/// <param name="array">The array to be written changes</param>
		/// <param name="arrayIndex">Start index</param>
		public void CopyTo(INode<T>[] array, int arrayIndex)
		{
			var i = 0; // current position
			var j = 0; // position in array
			foreach (var node in this)
			{
				if (array.Length > i)
				{
					if (i >= arrayIndex)
					{
						array[j] = node;
						j++;	
					}
					i++;
				}
				else break;
			}
		}

		/// <summary>
		/// Remove element from tree
		/// </summary>
		/// <param name="item"></param>
		/// <returns>True if deleted, otherwise false</returns>
		public bool Remove(INode<T> item)
		{
			return Remove(item.Data);
		}

		/// <summary>
		/// Remove element from tree
		/// </summary>
		/// <param name="data"></param>
		/// <returns>True if deleted, otherwise false</returns>
		public bool Remove(T data)
		{
			// first make sure there exist some items in this tree
			if (RootNode == null) return false; // no items to remove

			// Now, try to find data in the tree
			BinaryTreeNode<T>	current = RootNode, 
								parent	= null;

			var result = current.Data.CompareTo(data);
			while (result != 0)
			{
				if (result > 0)
				{
					// current.Value > data, if data exists it's in the left subtree
					parent = current;
					current = (BinaryTreeNode<T>) current.Left;
				}
				else if (result < 0)
				{
					// current.Value < data, if data exists it's in the right subtree
					parent = current;
					current = (BinaryTreeNode<T>) current.Right;
				}

				// If current == null, then we didn't find the item to remove
				if (current == null)
					return false;
				else
					result = current.Data.CompareTo(data);
			}

			// At this point, we've found the node to remove
			Count--;

			// We now need to "rethread" the tree
			// CASE 1: If current has no right child, then current's left child becomes
			//         the node pointed to by the parent
			if (current.Right == null)
			{
				RebuildTreeAfterDelete_Case1_WithoutRightChildren(parent, current);
			}

			// CASE 2: If current's right child has no left child, then current's right child
			//         replaces current in the tree
			else if (current.Right.Left == null)
			{
				RebuildTreeAfterDelete_Case2_ReplacingWithRightChild(parent, current);
			}

			// CASE 3:	If current's right child has a left child, replace current with current's
			//          right child's left-most descendent
			else
			{
				RebuildTreeAfterDelete_Case3_ReplacingWithLeftMostOfRightChildren(parent, current);
			}

			return true;
		}

		/// <summary>
		/// CASE 1: If current has no right child, then current's left child becomes 
		///			the node pointed to by the parent
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="current"></param>
		/// <param name="result"></param>
		private void RebuildTreeAfterDelete_Case1_WithoutRightChildren(IBinaryNode<T> parent, IBinaryNode<T> current)
		{
			if (parent == null)
			{
				RootNode = (BinaryTreeNode<T>)current.Left;
				
				if (RootNode == null) return;
				
				foreach (var node in RootNode)
				{
					node.Level--;
				}
			}
			else
			{
				var result = parent.Data.CompareTo(current.Data);
				if (result > 0)
					// parent.Value > current.Value, so make current's left child a left child of parent
					parent.Left = current.Left;
				else if (result < 0)
					// parent.Value < current.Value, so make current's left child a right child of parent
					parent.Right = current.Left;
				
				// all nodes deep level down
				if (current.Left == null) return;

				foreach (var node in current.Left)
				{
					if (node != null) node.Level--;
				}
			}
		}

		/// <summary>
		/// CASE 2:	If current's right child has no left child, then current's right child
		///			replaces current in the tree
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="current"></param>
		/// <param name="result"></param>
		private void RebuildTreeAfterDelete_Case2_ReplacingWithRightChild(IBinaryNode<T> parent, IBinaryNode<T> current)
		{
			current.Right.Left = current.Left;

			if (parent == null)
			{
				RootNode = (BinaryTreeNode<T>)current.Right;
				RootNode.Level--;
				
				if (RootNode.Right == null) return;

				foreach (var node in RootNode.Right)
				{
					node.Level--;
				}
			}
			else
			{
				var result = parent.Data.CompareTo(current.Data);
				if (result > 0)
					// parent.Value > current.Value, so make current's right child a left child of parent
					parent.Left = current.Right;
				else if (result < 0)
					// parent.Value < current.Value, so make current's right child a right child of parent
					parent.Right = current.Right;
				// node deep level down
				current.Right.Level--;
			}
		}

		/// <summary>
		/// CASE 3:	If current's right child has a left child, replace current with current's
		///			right child's left-most descendent
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="current"></param>
		private void RebuildTreeAfterDelete_Case3_ReplacingWithLeftMostOfRightChildren(IBinaryNode<T> parent, IBinaryNode<T> current)
		{
			// We first need to find the right node's left-most child
			BinaryTreeNode<T>	leftmost = (BinaryTreeNode<T>)current.Right.Left,
								lmParent = (BinaryTreeNode<T>)current.Right;
			
			var levelCoefficient = 2UL;
			while (leftmost.Left != null)
			{
				levelCoefficient++;
				lmParent = leftmost;
				leftmost = (BinaryTreeNode<T>)leftmost.Left;
			}

			// the parent's left subtree becomes the leftmost's right subtree
			lmParent.Left = leftmost.Right;

			// assign leftmost's left and right to current's left and right children
			leftmost.Left = current.Left;
			leftmost.Right = current.Right;

			if (parent == null)
			{
				RootNode = leftmost;
				RootNode.Level = 0UL;
				
				if (lmParent.Left == null) return;

				foreach (var node in lmParent.Left)
				{
					node.Level--;
				}
			}
			else
			{
				var result = parent.Data.CompareTo(current.Data);
				if (result > 0)
					// parent.Value > current.Value, so make leftmost a left child of parent
					parent.Left = leftmost;
				else if (result < 0)
					// parent.Value < current.Value, so make leftmost a right child of parent
					parent.Right = leftmost;
				leftmost.Level -= levelCoefficient;


				var queueForChecking = new Queue<IBinaryNode<T>>();
				var parents = new Queue<IBinaryNode<T>>();

				if (leftmost.Right	!= null) queueForChecking.Enqueue(leftmost.Right);

				var parentNode = leftmost;
				var level = leftmost.Level+1;
				var needChangeNode = true;

				IBinaryNode<T> currentNode = null;

				while (queueForChecking.Count != 0)
				{

					if (needChangeNode) currentNode = queueForChecking.Dequeue();
					if (parentNode.Left != currentNode && parentNode.Right != currentNode)
					{
						level++;
						parentNode = (BinaryTreeNode<T>) parents.Dequeue();
						needChangeNode = false;
						continue;
					}

					currentNode.Level = level;
					needChangeNode = true;
					if (currentNode.Left != null) queueForChecking.Enqueue(currentNode.Left);
					if (currentNode.Right != null) queueForChecking.Enqueue(currentNode.Right);
					if (currentNode.Left != null || currentNode.Right != null) parents.Enqueue(currentNode);
				}

			}
		}

		/// <summary>
		/// Check element if it contains in tree
		/// </summary>
		/// <param name="item">Element with data</param>
		/// <returns>True if contains, otherwise false</returns>
		public bool Contains(INode<T> item)
		{
			return Contains(item.Data);
		}

		/// <summary>
		/// Check element if it contains in tree
		/// </summary>
		/// <param name="data">Data for search</param>
		/// <returns>True if contains, otherwise false</returns>
		public bool Contains(T data)
		{
			// search the tree for a node that contains given data
			var current = RootNode;
			while (current != null)
			{
				var result = current.Data.CompareTo(data);
				if (result == 0)
					// we found data
					return true;
				else if (result > 0)
					// current.Value > data, search current's left subtree
					current = (BinaryTreeNode<T>) current.Left;
				else if (result < 0)
					// current.Value < data, search current's right subtree
					current = (BinaryTreeNode<T>) current.Right;
			}

			// didn't find data
			return false;
		}

		public INode<T> this[int index]
		{
			get
			{
				// if index is equal or greater than current count, then throw exception
				if( index >= Count ) throw new ArgumentOutOfRangeException("index");

				var idx = 0; // current index
				INode<T> selectedNode = null;
				foreach (var node in this)
				{
					if (idx != index) { idx++; continue; }

					selectedNode = node;
					break;
				}

				//if (selectedNode == null) throw new Exception("Item was not found in tree collection");
				return selectedNode;
			}
		}

		public IEnumerator<INode<T>> GetEnumerator()
		{
			if (RootNode == null) yield break;

			foreach (var node in RootNode)
			{
				yield return node;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// Creates full copy of current tree
		/// </summary>
		/// <returns><see cref="BinaryTree"/></returns>
		public object Clone()
		{
			var newBTree = new BinaryTree<T>();
			foreach (var node in this)
			{
				newBTree.Add(node.Data);
			}
			return newBTree;
		}

		public static bool operator ==(BinaryTree<T> tree1, BinaryTree<T> tree2)
		{
			if (Equals(tree1, null) ^ Equals(tree2, null)) return false;
			if (Equals(tree1, null) && Equals(tree2, null)) return true;
			if (tree1.Count != tree2.Count) return false;

			var treeArr1 = tree1.ToArray();
			var treeArr2 = tree2.ToArray();

			for (var i = 0; i < tree1.Count; i++)
			{
				if (treeArr1[i].Data.CompareTo(treeArr2[i].Data) != 0 ||
				    treeArr1[i].Level != treeArr2[i].Level)
				{
					return false;
				}
			}
			return true;
		}

		public static bool operator !=(BinaryTree<T> tree1, BinaryTree<T> tree2)
		{
			return !(tree1 == tree2);
		}

        //[TODO] create new traverse without recompile
		public void Traverse(Traversing eventHandler, TraverseType type = TraverseType.Preorder)
		{
			OnTraverse = null;
			OnTraverse += eventHandler;
			switch (type)
			{
				case TraverseType.Inorder:
					InorderTraversal(RootNode);
					break;
				case TraverseType.Postorder:
					PostoderTraversal(RootNode);
					break;
				case TraverseType.Width:
					WidthTraversal();
					break;
				case TraverseType.Preorder:
				default:
					PreorderTraversal(RootNode);
					break;
			}
		}

		private void PreorderTraversal(IBinaryNode<T> current)
		{
			if (current == null) return;

			// Output the value of the current node
			if (OnTraverse != null) OnTraverse(current);

			// Recursively print the left and right children
			PreorderTraversal(current.Left);
			PreorderTraversal(current.Right);
		}

		private void InorderTraversal(IBinaryNode<T> current)
		{
			if (current == null) return;

			// Visit the left child...
			InorderTraversal(current.Left);

			// Output the value of the current node
			if (OnTraverse != null) OnTraverse(current);

			// Visit the right child...
			InorderTraversal(current.Right);
		}

		private void PostoderTraversal(IBinaryNode<T> current)
		{
			if (current == null) return;

			// Visit the left child...
			PostoderTraversal(current.Left);

			// Visit the right child...
			PostoderTraversal(current.Right);

			// Output the value of the current node
			if (OnTraverse != null) OnTraverse(current);
		}

		private void WidthTraversal()
		{
			foreach (var node in RootNode)
			{
				if (OnTraverse != null) OnTraverse((IBinaryNode<T>) node);
			}
		}
	}
}