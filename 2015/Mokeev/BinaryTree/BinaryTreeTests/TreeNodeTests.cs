using System;
using System.Collections;
using BinaryTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryTreeTests
{
	[TestClass]
	public class TreeNodeTests
	{
		[TestMethod]
		public void Test_CreatingTreeNode_WithoutExtraData()
		{
			var node = new TreeNode<int>();

			Assert.AreEqual(node.Data, default(int));
			Assert.AreEqual(node.SubTrees, null);
			Assert.AreEqual(node.Level, 0UL);
		}

		[TestMethod]
		public void Test_CreatingTreeNode_WithData()
		{
			var node = new TreeNode<int>(5);

			Assert.AreEqual(node.Data, 5);
		}

		[TestMethod]
		public void Test_Enumerator_WithSubTrees()
		{
			var node = new TreeNode<int>(5) {SubTrees = new NodeList<int>(2)};
			node.SubTrees[0] = new BinaryTreeNode<int>(70);
			node.SubTrees[1] = new BinaryTreeNode<int>(50);

			INode<int> a = null;
			foreach (var node1 in ((IEnumerable) node))
			{
				a = node1 as INode<int>;
				break;
			}

			Assert.AreNotEqual(a, null);
			Assert.AreEqual(a.Data, 5);
		}

		[TestMethod]
		public void Test_Enumirator_WithZeroSubtrees()
		{
			var node = new TreeNode<int>(5);
			var node2 = new TreeNode<int>(4);
			var firstTree = new NodeList<int>(1);
			firstTree[0] = node2;
			var nl = new NodeList<int>(0);
			node2.SubTrees = nl;
			node.SubTrees = firstTree;

			INode<int> expectedNode = null;
			foreach (var treeNode in node)
			{
				expectedNode = treeNode;
			}

			Assert.AreNotEqual(expectedNode, null);
			Assert.AreEqual(expectedNode.Data, 4);
		}

		[TestMethod]
		public void Test_Enumerator_WithoutSubTrees()
		{
			var node = new TreeNode<int>(5) {SubTrees = new NodeList<int>(2)};
			node.SubTrees = null;

			INode<int> onlyOneNode = null;
			foreach (var treeNode in ((IEnumerable)node))
			{
				onlyOneNode = treeNode as INode<int>;
			}

			Assert.AreNotEqual(onlyOneNode, null);
			Assert.AreEqual(onlyOneNode.Data, 5);
		}
	}
}
