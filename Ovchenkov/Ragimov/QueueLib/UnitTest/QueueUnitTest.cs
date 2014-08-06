using System;
using System.Collections.Generic;
using QueueInterface;
using Xunit.Extensions;
using Assert = Xunit.Assert;

namespace UnitTest
{

    public class QueueUnitTest
    {
        public static IEnumerable<object[]> QueueTestData
        {
            get
            {
                yield return new object[] { new QueueLib.Queue<int>()};
                yield return new object[] { new QueueOnArray.Queue<int>()};
            }
        }

        [Theory]
        [PropertyData("QueueTestData")]
        public void Queue_EnqueuedDequeued(IQueueable<int> queue)
        {
            queue.Enqueue(42);
            Assert.Equal(42, queue.Dequeue());
        }

        [Theory]
        [PropertyData("QueueTestData")]
        public void Queue_DeletedFromEmpty_ShouldThrowInvalidOperation(IQueueable<int> queue)
        {
            Assert.Throws(typeof(InvalidOperationException), () =>
            {
                queue.Dequeue();
            });
        }

        [Theory]
        [PropertyData("QueueTestData")]
        public void Queue_CountIsProperlyChanged(IQueueable<int> queue)
        {
            queue.Enqueue(42);
            queue.Enqueue(37);
            queue.Enqueue(queue.Dequeue());
            Assert.Equal(2, queue.Count);
        }

        [Theory]
        [PropertyData("QueueTestData")]
        public void Queue_MultipleItemsStored(IQueueable<int> queue)
        {
            queue.Enqueue(42);
            queue.Enqueue(37);
            queue.Enqueue(100500);
            Assert.Equal(42, queue.Dequeue());
            Assert.Equal(37, queue.Dequeue());
            Assert.Equal(100500, queue.Dequeue());
        }

        [Theory]
        [PropertyData("QueueTestData")]
        public void Queue_Cleared(IQueueable<int> queue)
        {
            queue.Enqueue(42);
            queue.Enqueue(37);
            queue.Enqueue(100500);
            queue.Clear();
            Assert.Equal(0, queue.Count);
        }

        [Theory]
        [PropertyData("QueueTestData")]
        public void Queue_PeekedFromClear_ShouldThrowInvalidOperation(IQueueable<int> queue)
        {
            Assert.Throws(typeof(InvalidOperationException), () =>
            {
                queue.Enqueue(42);
                queue.Enqueue(37);
                queue.Enqueue(100500);
                queue.Clear();
                queue.Peek();
            });
        }

        [Theory]
        [PropertyData("QueueTestData")]
        public void Queue_PeekedAndDequeuedAreEqual(IQueueable<int> queue)
        {
            queue.Enqueue(42);
            queue.Enqueue(37);
            queue.Enqueue(100500);
            var temp = queue.Peek();
            Assert.Equal(temp, queue.Dequeue());
        }

        [Theory]
        [PropertyData("QueueTestData")]
        public void Queue_IEnumerableImplemented(IQueueable<int> queue)
        {
            int[] array = { 42, 37, 100500 };
            foreach (var t in array) //t -> temp
            {
                queue.Enqueue(t);
            }
            var i = 0;
            foreach (var temp in queue)
            {
                Assert.Equal(temp, array[i]);
                i++;
            }
        }

    }
}
