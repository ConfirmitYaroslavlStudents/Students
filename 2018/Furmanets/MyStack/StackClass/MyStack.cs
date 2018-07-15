using System;

namespace StackClass
{
    public class MyStack<T> where T : IComparable
    {
        private int _capasity;
        private T[] _mass;
        public int Count { get; private set; }

        public MyStack()
        {
            _capasity = 16;
            Count = 0;
            _mass = new T[_capasity];
        }

        public MyStack(int capasity)
        {
            _capasity = capasity;
            Count = 0;
            _mass = new T[_capasity];
        }

        public T Pop()
        {
            Count--;
            T result;

            try
            {
                result = _mass[Count];
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException("Stack Empty");
            }
            return result;
        }

        public T Peek()
        {
            T result;

            try
            {
                result = _mass[Count - 1];
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException("Stack Empty");
            }
            return result;
        }

        public void Push(T obj)
        {
            if (Count >= _capasity)
            {
                ReSize();
            }
            _mass[Count] = obj;
            Count++;
        }

        public bool Contains(T obj)
        {
            var index = Count;

            while (index > 0)
            {
                index--;
                if (_mass[index].Equals(obj))
                {
                    return true;
                }
            }

            return false;
        }

        public void Clear()
        {
            _capasity = 16;
            Count = 0;
            _mass = new T[_capasity];
        }

        private void ReSize()
        {
            _capasity *= _capasity;
            var newMass = new T[_capasity];

            for (var i = 0; i < Count; i++)
            {
                newMass[i] = _mass[i];
            }

            _mass = newMass;
        }
    }
}
