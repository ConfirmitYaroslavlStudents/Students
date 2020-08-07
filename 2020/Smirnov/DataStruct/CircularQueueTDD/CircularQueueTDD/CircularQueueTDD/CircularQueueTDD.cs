using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircularQueueTDD
{
    public class CircularQueue<T>
    {
        private T[] _items;
        private int _defaltCapasity = 10;
        private int _tail;
        private int _head;
        
        public CircularQueue()
        {
            Count = 0;
            _tail = -1;
            _head = 0;
            Capasity = _defaltCapasity;
            _items = new T[Capasity];
        }
        public CircularQueue(int capasity)
        {
            Count = 0;
            _tail = -1;
            _head = 0;
            Capasity = capasity;
            _items = new T[Capasity];
        }

        public int Count { get; private set; }
        public int Capasity { get; private set; }

        public void Enqueue(T item)
        {
            if(IsFull())
            {
                Resize();
            }
            Count++;
            _tail = (_tail + 1) % Capasity;
            _items[_tail] = item;
        }       
        public T Dequeue()
        {
            if (Count == 0)
                throw new InvalidOperationException("Queue is empty");
            Count--;
            T item = _items[_head];
            _head = (_head + 1) % Capasity;
            return item;
        }
        public T Peek()
        {
            return _items[_head];
        }
        private void Resize()
        {
            var NewArray = new T[Capasity * 2];
            if (_head < _tail)
            {
                Array.Copy(_items, _head, NewArray, 0, Capasity);
            }
            else
            {
                Array.Copy(_items, _head, NewArray, 0, Capasity - _head);
                Array.Copy(_items, 0, NewArray, Capasity - _head, _tail);
            }
            _head = 0;
            _tail = Count - 1;
            Capasity *= 2;           
            _items = NewArray;
        }
        private bool IsFull()
        {
            return _items.Length == Count;
        }

    }
}
