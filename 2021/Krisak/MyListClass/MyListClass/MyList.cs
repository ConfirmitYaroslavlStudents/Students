using System;

namespace MyListClass
{
    public class MyList<T>
    {
        public int Count { get; private set; }
        private T[] _mainArray;

        public MyList()
        {
            _mainArray = new T[4];
            Count = 0;
        }

        public T this[int i]
        {
            get
            {
                if (i >= Count || i < 0)
                    throw new IndexOutOfRangeException();
                return _mainArray[i];
            }
            set
            {
                if (i >= Count || i < 0)
                    throw new IndexOutOfRangeException();
                _mainArray[i] = value;
            }
        }

        private void Insert(T item, int index)
        {
            if (Count == _mainArray.Length)
                Array.Resize(ref _mainArray, Count * 2);

            for (var i = Count; i > index; i--)
                _mainArray[i] = _mainArray[i - 1];

            Count++;
            this[index] = item;
        }

        public void Add(T item)
        {
            Insert(item, Count);
        }

        public void AddIn(T item, int index)
        {
            Insert(item, index);
        }
    }
}
