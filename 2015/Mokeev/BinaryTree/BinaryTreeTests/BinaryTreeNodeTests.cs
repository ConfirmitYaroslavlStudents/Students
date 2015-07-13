using System;
using BinaryTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryTreeTests
{
	[TestClass]
	public class BinaryTreeNodeTests
	{
		[TestMethod]
		public void TestToString()
		{
			var node = new BinaryTreeNode<int>(50);
			Assert.AreEqual(node.ToString(), "50");
		}

		[TestMethod]
		public void TestCreatingNode()
		{
			var node1 = new BinaryTreeNode<int>(50);
			Assert.AreEqual(node1.Data, 50);

			var node2 = new BinaryTreeNode<int>(50, new BinaryTreeNode<int>(1), new BinaryTreeNode<int>(51));
			Assert.AreEqual(node2.Data, 50);
			Assert.AreEqual(node2.Left.Data, 1);
			Assert.AreEqual(node2.Right.Data, 51);
		}
	}
}
