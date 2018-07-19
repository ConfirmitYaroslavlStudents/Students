using Xunit;
using QueueConfirmitClass;

namespace UnitTests
{
    public class Tests
    {
        ArrayQueue<int> _arrayQueue = new ArrayQueue<int>();
        ListQueue<int> _listQueue = new ListQueue<int>();
        [Fact]
        public void CheckContainsArrayQueue()
        {
            AddToArrayQueue(5);
            Assert.True(_arrayQueue.Contains(1));
        }

        [Fact]
        public void CheckContainsListQueue()
        {
            AddToListQueue(5);
            Assert.True(_listQueue.Contains(1));
        }

        public void AddToArrayQueue(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _arrayQueue.Enqueue(i + 1);
            }
        }

        public void AddToListQueue(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _listQueue.Enqueue(i + 1);
            }
        }

        [Fact]
        public void CheckCountArrayQueue()
        {
            AddToArrayQueue(5);
            Assert.Equal(5,_arrayQueue.Count());
        }

        [Fact]
        public void CheckCountListQueue()
        {
            AddToListQueue(5);
            Assert.Equal(5, _listQueue.Count());
        }

        [Fact]
        public void CheckPeekArrayQueue()
        {
            AddToArrayQueue(5);
            Assert.Equal(1,_arrayQueue.Peek());
        }

        [Fact]
        public void CheckPeekListQueue()
        {
            AddToListQueue(5);
            Assert.Equal(1,_listQueue.Peek());
        }

        [Fact]
        public void CheckDequeueArrayQueue()
        {
            AddToArrayQueue(5);
            _arrayQueue.Dequeue();
            Assert.Equal(2,_arrayQueue.Peek());
        }

        [Fact]
        public void CheckDequeueListQueue()
        {
            AddToListQueue(5);
            _listQueue.Dequeue();
            Assert.Equal(2,_listQueue.Peek());
        }

        [Fact]
        public void CheckFullArrayQueue()
        {
            AddToArrayQueue(5);
            for (int i = 5; i < 21; i++)
                _arrayQueue.Enqueue(i);
            _arrayQueue.Enqueue(22);
            Assert.Equal(20,_arrayQueue.Count());
        }

        [Fact]
        public void CheckClearArrayQueue()
        {
            AddToArrayQueue(5);
            _arrayQueue.Clear();
            Assert.Equal(0,_arrayQueue.Count());
        }

        [Fact]
        public void CheckClearListQueue()
        {
            AddToListQueue(5);
            _listQueue.Clear();
            Assert.Equal(0,_listQueue.Count());
        }
    }
}
