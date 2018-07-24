using System;

namespace QueueConfirmitClass
{

    public class ArrayQueue<T> : MyQueue<T> where T : IComparable<T>
    {
        private int _maxSize = 20;
        private T[] _queue;
        private int _front;
        private int _rear;

        public ArrayQueue()
        {
            _queue = new T[_maxSize];
            _front = _rear = -1;
        }
        public override void Enqueue(T element)
        {
            if (IsFull())
            {
                Resize();
            }

            if (IsEmpty())
            {
                _front = _rear = 0;
            }
            else
                _rear = (_rear + 1) % _maxSize;

            _queue[_rear] = element;
        }

        public void Resize()
        {
            T[] newArray = new T[_maxSize + 20];
            int i = -1;
            while (_front != _rear)
            {
                i++;
                newArray[i] = _queue[_front];
                _front = (_front + 1) % _maxSize;
            }
            newArray[i + 1] = _queue[_rear];
            _queue = newArray;
            _rear = i + 1;
            _front = 0;
            _maxSize = _queue.Length;
        }

        public bool IsFull()
        {
            return ((_rear + 1) % _maxSize == _front) ? true : false;
        }
        public bool IsEmpty()
        {
            return (_front == -1 && _rear == -1);
        }

        public override void Dequeue()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Queue is empty!");
                return;
            }
            if (_rear == _front)
                _rear = _front = -1;
            else
                _front = (_front + 1) % _maxSize;
        }

        public override T Peek()
        {
            if(_front == -1)
            {
                Console.WriteLine("Queue is empty. Have no element to peek");
                return default(T);
            }
            return _queue[_front];
        }

        public void Print()
        {
            int count = Count(); 
            Console.WriteLine("Printing queue...");
            for(int i = 0; i < count; i++)
            {
                int index = (_front + i) % _maxSize;
                Console.Write("{0} ", _queue[i]);
            }
            Console.WriteLine();
        }

        public override void Clear()
        {
            _front = _rear = -1;
        }

        public override int Count()
        {
            if (_rear == -1 && _front == -1)
                return (_rear + _maxSize - _front) % _maxSize;
            return (_rear + _maxSize - _front) % _maxSize + 1;
        }

        public override bool Contains(T element)
        {
            return Array.Exists(_queue, elem => elem.CompareTo(element) == 0);
        }
    }
}
