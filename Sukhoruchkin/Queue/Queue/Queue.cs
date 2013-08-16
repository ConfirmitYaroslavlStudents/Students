using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;

namespace Queue
{
    public class Queue<T> : IEnumerable<T>, IEnumerable
    {
        private int _count;
        private Element<T> _first;
        private Element<T> _last;
        public int Count
        { get { return _count; } }
        private Queue()
        {
            this._count = 0;
            this._first = null;
            this._last = null;
        }
        public static Queue<T> GetQueue()
        {
             return Singleton<Queue<T>>.Instance; 
        }
        public void Enqueue(T item)
        {
            Element<T> newElementInQueue = new Element<T>(item);
            try
            {
                _last.Next = newElementInQueue;
                _last = newElementInQueue;
            }
            catch
            {
                _first = newElementInQueue;
                _last = _first;
            }
            _count++;
        }
        public T Dequeue()
        {
            try
            {
                T elementInQueue = _first.Value;
                if (_count==1)
                {
                    _first = null;
                    _last = null;
                }
                else
                {
                    _first = _first.Next;
                }  
                _count--;
                return elementInQueue;
            }
            catch
            {
                throw new InvalidOperationException("Queue empty.");
            }
        }
        public void Clear()
        {
            _count = 0;
            _first = null;
            _last = null;
        }
        public T Peek()
        {
            try
            {
                return _first.Value;
            }
            catch
            {
                throw new InvalidOperationException("Queue empty.");
            }
        }
        public T[] ToArray()
        {
            Element<T> arrayElement = _first;
            T[] queueArray = new T[_count];
            for (int i = 0; i < _count;i++)
            {
                queueArray[i] = arrayElement.Value;
                arrayElement = arrayElement.Next;
            }
            return queueArray;
        }
        private T GetElement(int index)
        {
            Element<T> queueElement = _first;
            for (int i = 0; i < index; i++)
            {
                queueElement = queueElement.Next;
            }
            return queueElement.Value;
        }
        public Queue<T>.Enumerator GetEnumerator()
        {
            return new Queue<T>.Enumerator(this);
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>)new Queue<T>.Enumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)new Queue<T>.Enumerator(this);
        }
        public class Enumerator : IEnumerator<T>, IDisposable, IEnumerator
        {
            private Queue<T> _queue;
            private int _index;
            private T _currentElement;
            private T[] _elementsInQueue;
            public Enumerator(Queue<T> queue)
            {
                this._queue = queue;
                this._index = -1;
                this._currentElement = default(T);
                this._elementsInQueue = _queue.ToArray();
            }
            public T Current
            {
                get
                {
                    if (this._index < 0)
                    {
                        throw new InvalidOperationException("EnumNotStarted.");
                    }
                    return this._currentElement;
                }
            }
            object IEnumerator.Current
            {
                get
                {
                    if (this._index < 0)
                    {
                        throw new InvalidOperationException("EnumNotStarted.");
                    }
                    return (object)this._currentElement;
                }
            }
            public void Dispose()
            {
                this._index = -1;
                this._currentElement = default(T);
            }
            public bool MoveNext()
            {
                if (this._index == _queue._count - 1)
                {
                    return false;
                }
                else
                {
                    this._currentElement = _elementsInQueue[++_index];
                    return true;
                }
            }
            void IEnumerator.Reset()
            {
                this._index = -1;
                this._currentElement = default(T);
            }

        }
    }
    class Element<T>
    {
        private T _value;
        private Element<T> _next;
        public Element(T value)
        {
            this._value = value;
        }
        public T Value
        {
            get { return _value; }
        }
        public Element<T> Next
        {
            get { return _next; }
            set { _next = value; }
        }
    }
}


