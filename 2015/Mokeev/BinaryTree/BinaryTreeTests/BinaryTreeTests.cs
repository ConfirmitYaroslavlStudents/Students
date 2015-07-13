using System;
using System.Collections;
using System.Linq;
using System.Text;
using BinaryTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryTreeTests
{
	[TestClass]
	public class BinaryTreeTests
	{
		[TestMethod]
		public void Test_InsertingInBinaryTree()
		{
			var tree = new BinaryTree<int> {3, new BinaryTreeNode<int>(5), 10};

			Assert.AreEqual(tree[0].Data, 3);
			Assert.AreEqual(tree[2].Data, 10);
			Assert.AreEqual(tree[1].Data, 5);

			tree.Clear();

			tree.Add(90);
			tree.Add(50);
			tree.Add(150);
			tree.Add(20);
			tree.Add(75);
			tree.Add(5);
			tree.Add(25);

			tree.Add(62);

			Assert.AreEqual(tree[7].Data, 62);

			Assert.AreEqual(tree[0].SubTrees[0].SubTrees[1].SubTrees[0].Data, 62);


		}

		[TestMethod]
		public void Test_IClonable()
		{
			var oldTree = new BinaryTree<int>()
			{
				1,2,5,-5,-1000,50,70,-8,-5,-40,150,140,80
			};

			var clonedTree = (BinaryTree<int>) oldTree.Clone();

			// check all values from old tree
			foreach (var node in clonedTree)
			{
				Assert.AreEqual(oldTree.Contains(node.Data), true);
			}

			// check all values from new tree
			foreach (var node in oldTree)
			{
				Assert.AreEqual(clonedTree.Contains(node.Data), true);
			}
		}

		[TestMethod]
		public void Test_NullAddToTree()
		{
			
			var tree = new BinaryTree<string>();
			tree.Add((INode<string>)null);

			Assert.AreEqual(tree.Count, 0);
		}

		[TestMethod]
		public void Test_ClearingTree()
		{
			var tree = new BinaryTree<int>() {1, 5, 3, 4, 9};
			Assert.AreEqual(tree.Count, 5);

			tree.Clear();
			Assert.AreEqual(tree.Count, 0);

			var count = tree.Count();

			Assert.AreEqual(count, 0);
		}

		[TestMethod]
		public void Test_ContainsInTree()
		{
			var tree = new BinaryTree<int>() { 1, 5, 3, 4, 9 };

			Assert.AreEqual(tree.Contains(new BinaryTreeNode<int>(5)), true);
			Assert.AreEqual(tree.Contains(new BinaryTreeNode<int>(77)), false);

			Assert.AreEqual(tree.Contains(5), true);
			Assert.AreEqual(tree.Contains(77), false);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException), "index")]
		public void Test_Iterator_ArgumentOutOfRange()
		{
			var tree = new BinaryTree<int>() { 1, 5, 3, 4, 9 };
			var test = tree[5];
		}

		[TestMethod]
		public void Test_IsReadOnly()
		{
			var tree = new BinaryTree<int>();
			Assert.AreEqual(tree.IsReadOnly, false);
		}

		[TestMethod]
		public void Test_CopyTo()
		{
			var tree = new BinaryTree<int>() { 90, 50,150, 20, 75 };
			var arr = new INode<int>[5];
			tree.CopyTo(arr,0);

			INode<int>[] expected = {
				new BinaryTreeNode<int>(90),
				new BinaryTreeNode<int>(50),
				new BinaryTreeNode<int>(150),
				new BinaryTreeNode<int>(20),
				new BinaryTreeNode<int>(75),
			};

			for (var i = 0; i < arr.Length; i++)
			{
				Assert.AreEqual(arr[i].Data, expected[i].Data);
			}

			var arr2 = new INode<int>[1];
			tree.CopyTo(arr2,0);
			Assert.AreEqual(arr2[0].Data, 90);
			Assert.AreEqual(arr2.Length, 1);
		}


		[TestMethod]
		public void Test_Remove_T_CASE1_1()
		{
			// CASE 1: If current has no right child, then current's left child becomes
			//         the node pointed to by the parent

			// 1st
			var tree = new BinaryTree<int> { 90, 50, 150, 20, 5, 25 };
			tree.Remove(50);
			var expectedTree = new BinaryTree<int>() { 90, 20, 150, 5, 25 };

			Assert.AreEqual(true, tree == expectedTree);
		}

		[TestMethod]
		public void Test_Remove_T_CASE1_2()
		{
			// CASE 1: If current has no right child, then current's left child becomes
			//         the node pointed to by the parent

			// 2nd
			var tree = new BinaryTree<int> { 90, 50, 20, 5, 25 };
			tree.Remove(90);
			var expectedTree = new BinaryTree<int> { 50, 20, 5, 25 };
			Assert.AreEqual(true, expectedTree == tree);
		}

		[TestMethod]
		public void Test_Remove_T_CASE2_1()
		{

			// CASE 2: If current's right child has no left child, then current's right child
			//         replaces current in the tree

			// 1st
			var tree = new BinaryTree<int> { 90, 50, 150, 20, 125, 175, 5, 25, 140 };
			tree.Remove(150);
			var expectedTree = new BinaryTree<int> { 90, 50, 175, 20, 125, 5, 25, 140 };

			Assert.AreEqual(true, tree == expectedTree);
		}

		[TestMethod]
		public void Test_Remove_T_CASE2_2()
		{

			// CASE 2: If current's right child has no left child, then current's right child
			//         replaces current in the tree

			// 2nd
			var tree = new BinaryTree<int> { 90, 50, 150, 175 };
			tree.Remove(90);
			var expectedTree = new BinaryTree<int> { 150, 50, 175 };
			Assert.AreEqual(true, expectedTree == tree);
		}

		[TestMethod]
		public void Test_Remove_T_CASE3_1()
		{
			// CASE 3:	If current's right child has a left child, replace current with current's
			//          right child's left-most descendent

			var tree = new BinaryTree<int> { 90, 50, 150, 20, 75, 5, 66, 80, 68 };
			tree.Remove(50);
			var expectedTree = new BinaryTree<int> { 90, 66, 150, 20, 75, 5, 68, 80 };

			Assert.AreEqual(true, tree == expectedTree);
		}

		[TestMethod]
		public void Test_Remove_T_CASE3_2()
		{
			// CASE 3:	If current's right child has a left child, replace current with current's
			//          right child's left-most descendent

			var tree = new BinaryTree<int> { 90, 50, 150, 20, 75, 5, 66, 80, 68, 81 };
			tree.Remove(50);
			var expectedTree = new BinaryTree<int> { 90, 66, 150, 20, 75, 5, 68, 80, 81 };

			Assert.AreEqual(true, tree == expectedTree);
		}

		[TestMethod]
		public void Test_Remove_T_CASE3_3()
		{
			// CASE 3:	If current's right child has a left child, replace current with current's
			//          right child's left-most descendent
			var tree = new BinaryTree<int> { 90, 50, 150, 20, 75, 125, 175, 5, 25, 66, 80, 140, 68, 123 };
			tree.Remove(90);
			var expectedTree = new BinaryTree<int> {123, 50, 150, 20, 75, 125, 175, 5, 25, 66, 80, 140, 68};

			Assert.AreEqual(true, tree == expectedTree);
		}

		[TestMethod]
		public void Test_Remove_T_CASE3_4()
		{
			// CASE 3:	If current's right child has a left child, replace current with current's
			//          right child's left-most descendent
			var tree = new BinaryTree<int> { 90, 50, 150, 20, 75, 5, 66, 80, 68 };
			tree.Remove(90);
			var expectedTree = new BinaryTree<int> { 150, 50, 20, 75, 5, 66, 80, 68 };

			Assert.AreEqual(true, tree == expectedTree);
		}

		[TestMethod]
		public void Test_Remove_T_CASE3_5()
		{
			// CASE 3:	If current's right child has a left child, replace current with current's
			//          right child's left-most descendent

			var tree = new BinaryTree<int> { 90, 50, 150, 20, 75, 125, 175, 5, 66, 80, 123, 140, 68, 124 };
			tree.Remove(90);
			var expectedTree = new BinaryTree<int> { 123, 50, 150, 20, 75, 125, 175, 5, 66, 80, 124, 140, 68 };

			Assert.AreEqual(true, tree == expectedTree);
		}

		[TestMethod]
		public void Test_Remove_T()
		{
			//false, if tree is empty
			var tree = new BinaryTree<int>();
			Assert.AreEqual(false, tree.Remove(0));

			//false if there is no element like given
			tree.Add(7);
			Assert.AreEqual(false, tree.Remove(8));

		}

		[TestMethod]
		public void Test_Remove_Item()
		{
			var tree = new BinaryTree<int>() { new BinaryTreeNode<int>(5) };
			Assert.AreEqual(true, tree.Remove(new BinaryTreeNode<int>(5)));
			Assert.AreEqual(0, tree.Count);
		}

		[TestMethod]
		public void Test_Equals()
		{
			var t1 = new BinaryTree<int>() { 90, 50, 150, 20, 5, 25 };
			var t2 = new BinaryTree<int>() { 90, 50, 150, 20, 5, 25 };
			var t3 = new BinaryTree<int>() { 90, 50, 150, 20, 5 };
			var t4 = new BinaryTree<int>() { 90, 50, 150, 20, 5, 26 };
			var t5 = new BinaryTree<int>() { 90, 50, 150, 20, 5, 25, 27 };

			BinaryTree<int> t6 = null;
			BinaryTree<int> t7 = null;
			
			Assert.AreEqual(t1 == t2, true);
			Assert.AreEqual(t1 == t3, false);
			Assert.AreEqual(t1 == t4, false);
			Assert.AreEqual(t1 != t5, true);
			Assert.AreEqual(t6 != t7, false);
			Assert.AreEqual(t1 == t7, false);
			Assert.AreEqual(t7 == t1, false);
		}

		[TestMethod]
		public void Test_Enumerator()
		{
			var tree = new BinaryTree<int>() { new BinaryTreeNode<int>(5) };
			INode<int> test = null;
			foreach (INode<int> node in ((IEnumerable)tree))
			{
				test = node;
			}

			Assert.AreNotEqual(null, test);
			Assert.AreEqual(5, test.Data);
		}

		[TestMethod]
		public void Test_Enumerator_OfEmptyTree()
		{
			var tree = new BinaryTree<int>();
			INode<int> nullNode = null;

			foreach (var node in tree)
			{
				nullNode = node;
			}

			Assert.IsNull(nullNode);
		}

		[TestMethod]
		public void Test_SameValuesAddAndRemove()
		{
			var tree = new BinaryTree<int>(){5,5,7,3,5};
			Assert.AreEqual(true, tree.Remove(5));
			Assert.AreEqual(true, tree.Remove(5));
			Assert.AreEqual(true, tree.Remove(5));
			Assert.AreEqual(true, tree.Remove(7));
			Assert.AreEqual(3, tree[0].Data);
		}

		[TestMethod]
		public void Test_Preorder_Traverse()
		{
			var tree = new BinaryTree<int> {90, 50, 150, 20, 75, 95, 175, 5, 25, 66, 80, 92, 111, 166, 200};
			var data = new StringBuilder();
			tree.Traverse(node=> data.Append(node+" "), TraverseType.Preorder);

			Assert.AreEqual("90 50 20 5 25 75 66 80 150 95 92 111 175 166 200 ", data.ToString());
		}

		[TestMethod]
		public void Test_Inorder_Traverse()
		{
			var tree = new BinaryTree<int> { 90, 50, 150, 20, 75, 95, 175, 5, 25, 66, 80, 92, 111, 166, 200 };
			var data = new StringBuilder();
			tree.Traverse(node => data.Append(node + ", "), TraverseType.Inorder);

			Assert.AreEqual("5, 20, 25, 50, 66, 75, 80, 90, 92, 95, 111, 150, 166, 175, 200, ", data.ToString());
		}

		[TestMethod]
		public void Test_Postorder_Traverse()
		{
			var tree = new BinaryTree<int> { 90, 50, 150, 20, 75, 95, 175, 5, 25, 66, 80, 92, 111, 166, 200 };
			var data = new StringBuilder();
			tree.Traverse(node => data.Append(node + ", "), TraverseType.Postorder);

			Assert.AreEqual("5, 25, 20, 66, 80, 75, 50, 92, 111, 95, 166, 200, 175, 150, 90, ", data.ToString());
		}

		[TestMethod]
		public void Test_Width_Traverse()
		{
			var tree = new BinaryTree<int> { 90, 50, 150, 20, 75, 95, 175, 5, 25, 66, 80, 92, 111, 166, 200 };
			var data = new StringBuilder();
			tree.Traverse(node => data.Append(node + ", "), TraverseType.Width);

			Assert.AreEqual("90, 50, 150, 20, 75, 95, 175, 5, 25, 66, 80, 92, 111, 166, 200, ", data.ToString());
		}
	}
}
