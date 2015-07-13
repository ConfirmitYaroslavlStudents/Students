using System;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queue;

namespace QueueFromListTests
{
    [TestClass]
    public class QueueFromListDequeue
    {
        [TestMethod]
        public void Dequeue_QueueIsEmpty_ShouldThrowExeption()
        {
            // arrange
            QueueFromList<string> actual = new QueueFromList<string>();
            
            // act
            try
            {
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
        public void Dequeue_QueueHasOneItem()
        {
            // arrange
            QueueFromList<string> actual = new QueueFromList<string>("cat");
            QueueFromList<string> expected = new QueueFromList<string>();
            const string expectedWord = "cat";

            // act
            string actualWord = actual.Dequeue();

            // assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedWord, actualWord);
        }

        [TestMethod]
        public void Dequeue_QueueHasMoreThanOneItem()
        {
            // arrange
            QueueFromList<string> actual = new QueueFromList<string>("cat", "dog", "bird");
            QueueFromList<string> expected = new QueueFromList<string>("dog", "bird");
            const string expectedWord = "cat";
            // act
            string actualWord = actual.Dequeue();

            // assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedWord, actualWord);
        }
    }
}
