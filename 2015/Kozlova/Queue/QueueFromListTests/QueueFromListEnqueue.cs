using System;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queue;

namespace QueueFromListTests
{
    [TestClass]
    public class QueueFromListEnqueue
    {
        [TestMethod]
        public void Enqueue_HeadIsNull()
        {
            // arrange
            QueueFromList<int> actual = new QueueFromList<int>();
            
            // act
            actual.Enqueue(5);

            // assert
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual("5 ", actual.ToString());
        }

        [TestMethod]
        public void Enqueue_HeadIsNotNull()
        {
            // arrange
            QueueFromList<int> actual = new QueueFromList<int>(5,6,7);
            
            // act
            actual.Enqueue(8);

            // assert
            Assert.AreEqual(4, actual.Count);
            Assert.AreEqual("5 6 7 8 ", actual.ToString());
        }
    }
}
