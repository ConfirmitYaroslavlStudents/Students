using System;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queue;

namespace QueueTests
{
    [TestClass]
    public class QueueTestsDequeue
    {
        [TestMethod]
        public void Dequeue_QueueIsEmpty_ShouldThrowExeption()
        {
            // arrange
            Queue<string> actual = new Queue<string>("cat");
            

            // act
            try
            {
                actual.Dequeue();
                actual.Dequeue();
            }
            catch (Exception e)
            {
                // assert
                StringAssert.Contains(e.Message, "Queue is empty!");
                return;
            }
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod]
        public void Dequeue_QueueIsNotEmpty()
        {
            // arrange
            Queue<string> actual = new Queue<string>("cat");
            string expectedString = "cat";
            Queue<string> expected = new Queue<string>();

            // act
            string actualString = actual.Dequeue();

            // assert
            Assert.AreEqual(expectedString, actualString);
            Assert.AreEqual(expected, actual);
        }
    }
}
