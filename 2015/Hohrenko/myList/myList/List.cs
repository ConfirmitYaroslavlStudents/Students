using System.Collections;
using System.Collections.Generic;

namespace MyList
{
    public class TEnum<T> : IEnumerator<T>
    {
        private T[] _elements;
        private int _position = -1;

        public TEnum(T[] items)
        {
            _elements = items;
        }

        public bool MoveNext()
        {
            _position++;
            return (_position < _elements.Length);
        }

        public void Reset()
        {
            _position = -1;
        }

        public T Current {
            get { return _elements[_position]; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
            
        }
    }

    //[TODO] implement with linked nodes
    public class List<T> : IEnumerable<T>
    {
        private T[] _contents = new T[8];
        private int _count;

        public List()
        {
            _count = 0;
        }

        public void Add(T item)
        {
            if (_count >= _contents.Length)
                Expand();
            _contents[_count] = item;
            _count++;
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        private void Expand()
        {
            T[] temp = _contents;
            _contents = new T[Count * 2];
            temp.CopyTo(_contents, 0);
        }

        public void Clear()
        {
            _count = 0;
            _contents = null;
        }

        public int IndexOf(T item)
        {
            int itemIndex = -1;
            for (int i = 0; i < Count; i++)
            {
                if (item.Equals(_contents[i]))
                {
                    itemIndex = i;
                    break;
                }
            }
            return itemIndex;
        }

        public bool Contains(T item)
        {
            if (IndexOf(item) != -1)
                return true;
            return false;
        }

        public void Remove(T item)
        {
            RemoveAt(IndexOf(item));
        }

        public int Count
        {
            get { return _count; }
        }

        public void Insert(int index, T item)
        {
            if ((index < Count) && (index >= 0))
            {
                if (_count + 1 >= _contents.Length)
                    Expand();
                _count++;

                for (int i = Count - 1; i > index; i--)
                {
                    _contents[i] = _contents[i - 1];
                }
                _contents[index] = item;
            }
        }

        public void RemoveAt(int index)
        {
            if ((index >= 0) && (index < Count))
            {
                for (int i = index; i < Count - 1; i++)
                {
                    _contents[i] = _contents[i + 1];
                }
                _count--;
            }
        }

        public T this[int index]
        {
            get
            {
                return _contents[index];
            }
            set
            {
                _contents[index] = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {         
            return new TEnum<T>(_contents);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
