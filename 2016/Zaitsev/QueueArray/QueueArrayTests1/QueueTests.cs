using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueueArray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueArray.Tests
{
	[TestClass]
	public class QueueTests
	{
		[TestMethod]
		public void Push_1and2_1and2returned()
		{
			Queue<int> queue = new Queue<int>();
			int[] expected = { 1, 2 };

			queue.Push(1);
			queue.Push(2);

			Assert.AreEqual(expected.Length, queue.Count, "Size is not right!");
			CollectionAssert.AreEqual(expected, queue.GetElements());
		}

		[TestMethod]
		public void Push_OneMoreCapacityElements_SizeIsDoubled()
		{
			int capacity = 3;
			Queue<int> queue = new Queue<int>(capacity);
			int[] expectedQueue = new int[capacity + 1];

			for (int i = 0; i < capacity + 1; i++)
			{
				queue.Push(i);
				expectedQueue[i] = i;
			}

			Assert.AreEqual(capacity * 2, queue.Capacity, "Capacity is not doubled!");
			Assert.AreEqual(expectedQueue.Length, queue.Count, "Size is not right!");
			CollectionAssert.AreEqual(expectedQueue, queue.GetElements());
		}

		[TestMethod]
		public void IsEmpty_NoElements_True()
		{
			Queue<object> queue = new Queue<object>();
			Assert.AreEqual(true, queue.IsEmpty());
		}

		[TestMethod()]
		public void IsEmpty_1add_False()
		{
			Queue<object> queue = new Queue<object>();
			queue.Push("string");
			Assert.AreEqual(false, queue.IsEmpty());
		}

		[TestMethod]
		public void IsFull_FullQueue_True()
		{
			Queue<int> queue = new Queue<int>(1);
			queue.Push(1);
			Assert.AreEqual(true, queue.IsFull());
		}

		[ExpectedException(typeof(Exception), "Exception was not thrown!")]
		[TestMethod]
		public void Pop_NoElements_Exception()
		{
			Queue<int> queue = new Queue<int>();
			queue.Pop();
		}

		[TestMethod]
		public void Pop_3214elements_3returned()
		{
			Queue<int> queue = new Queue<int>();
			queue.Push(3);
			queue.Push(2);
			queue.Push(1);
			queue.Push(4);
			int[] expectedQueue = {2, 1, 4};

			int actualNumber = queue.Pop();

			Assert.AreEqual(3, actualNumber);
			CollectionAssert.AreEqual(expectedQueue, queue.GetElements());
		}

		[TestMethod]
		public void PopAndPush_LoopQueue()
		{
			int capacity = 3;
			Queue<int> queue = new Queue<int>(capacity);
			int[] expectedQueue = { 1, 4, 5 };

			queue.Push(3);
			queue.Push(2);
			queue.Push(1);
			queue.Pop();
			queue.Push(4);
			queue.Pop();
			queue.Push(5);

			Assert.AreEqual(capacity, queue.Capacity, "Capacity is increased!");
			CollectionAssert.AreEqual(expectedQueue, queue.GetElements());
		}

		[TestMethod]
		public void PopAndPush_LoopAndDoubleQueue()
		{
			int capacity = 3;
			Queue<int> queue = new Queue<int>(capacity);
			int[] expectedQueue = { 2, 1, 4, 5 };

			queue.Push(3);
			queue.Push(2);
			queue.Push(1);
			queue.Pop();
			queue.Push(4);
			queue.Push(5);

			Assert.AreEqual(capacity * 2, queue.Capacity, "Capacity is not increased!");
			CollectionAssert.AreEqual(expectedQueue, queue.GetElements());
		}
	}
}