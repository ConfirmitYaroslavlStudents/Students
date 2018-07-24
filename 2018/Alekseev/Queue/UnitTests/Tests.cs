using Xunit;
using QueueConfirmitClass;

namespace UnitTests
{
    public class Tests
    {
        [Fact]
        public void CheckContainsQueue()
        {
            MyQueue<int> queueArray = new ArrayQueue<int>();
            MyQueue<int> queueList = new ListQueue<int>();
            for (int i = 1; i < 6; i++)
            {
                queueArray.Enqueue(i);
                queueList.Enqueue(i);
            }

            var resultArray = queueArray.Contains(1);
            var resultList = queueList.Contains(1);

            Assert.True(resultArray);
            Assert.True(resultList);

        }      
        [Fact]
        public void CheckCountQueue()
        {
            MyQueue<int> queueArray = new ArrayQueue<int>();
            MyQueue<int> queueList = new ListQueue<int>();
            for (int i = 1; i < 11; i++)
            {
                queueArray.Enqueue(i);
                queueList.Enqueue(i);
            }

            var countArray = queueArray.Count();
            var countList = queueList.Count();

            Assert.Equal(10,countArray);
            Assert.Equal(10, countList);
        }

        [Fact]
        public void CheckPeekQueue()
        {
            MyQueue<int> queueArray = new ArrayQueue<int>();
            MyQueue<int> queueList = new ListQueue<int>();
            for (int i = 1; i < 11; i++)
            {
                queueArray.Enqueue(i);
                queueList.Enqueue(i);
            }

            var resultArray = queueArray.Peek();
            var resultList = queueList.Peek();

            Assert.Equal(1,resultArray);
            Assert.Equal(1,resultList);
        }

        [Fact]
        public void CheckDequeueQueue()
        {
            MyQueue<int> queueArray = new ArrayQueue<int>();
            MyQueue<int> queueList = new ListQueue<int>();
            for (int i = 1; i < 11; i++)
            {
                queueArray.Enqueue(i);
                queueList.Enqueue(i);
            }

            queueArray.Dequeue();
            queueList.Dequeue();

            Assert.Equal(2, queueArray.Peek());
            Assert.Equal(2, queueList.Peek());
        }

        [Fact]
        public void CheckClearQueue()
        {
            MyQueue<int> queueArray = new ArrayQueue<int>();
            MyQueue<int> queueList = new ListQueue<int>();
            for (int i = 1; i < 11; i++)
            {
                queueArray.Enqueue(i);
                queueList.Enqueue(i);
            }

            queueArray.Clear();
            queueList.Clear();

            Assert.Equal(0,queueArray.Count());
            Assert.Equal(0,queueList.Count());
        }

        [Fact]
        public void CheckOverflowArrayQueue()
        {
            MyQueue<int> queueArray = new ArrayQueue<int>();
            for (int i = 1; i <= 21; i++)
            {
                queueArray.Enqueue(i);
            }

            var resultArray = queueArray.Count();

            Assert.Equal(21, resultArray);
        }

        [Fact]
        public void CheckOverflowArrayQueueAfterDequeue()
        {
            MyQueue<int> queueArray = new ArrayQueue<int>();
            for (int i = 1; i <= 20; i++)
            {
                queueArray.Enqueue(i);
            }
            queueArray.Dequeue();
            for (int i = 21; i <= 22; i++)
            {
                queueArray.Enqueue(i);
            }

            var resultArray = queueArray.Count();

            Assert.Equal(21, resultArray);
        }
    }
}
