using System;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queue;

namespace QueueTests
{
    [TestClass]
    public class QueueTestsEnqueue
    {
        [TestMethod]
        public void Enqueue_NextIndexLessThanArrayLength()
        {
            // arrange
            Queue<int> actual = new Queue<int>();
            
            // act
            actual.Enqueue(5);

            // assert
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual("5 ", actual.ToString());
        }

        [TestMethod]
        public void Enqueue_NextIndexGreaterThanArrayLength_ResizeArray()
        {
            // arrange
            Queue<int> actual = new Queue<int>(1, 2, 3, 4, 5);
            
            // act
            actual.Enqueue(6);

            // assert
            Assert.AreEqual(6, actual.Count);
            Assert.AreEqual("1 2 3 4 5 6 ", actual.ToString());
            Assert.AreEqual(10, actual.Items.Length);
        }
    }
}
