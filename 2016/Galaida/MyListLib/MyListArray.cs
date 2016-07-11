using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyListLib
{
    public class MyListArray<T> : IEnumerable<T>
    {
        private T[] _list;
        public int Count { get; set; }

        public MyListArray(IEnumerable<T> list)
        {
            if (list == null || !list.Any())
            {
                _list = null;
                return;
            }

            _list = list.ToArray();
            Count = _list.Length;
        }

        public void Add(T value)
        {
            var newList = new T[_list.Length + 1];
            for(int i=0; i<_list.Length; i++)
            {
                var temp = _list[i];
                newList[i] = temp;
            }
            newList[_list.Length] = value;
            _list = newList;
            Count = newList.Length;
        }

        public void Clear()
        {
            _list = null;
            Count = 0;
        }

        public void Insert(int i, T value)
        {
            var newList = new T[_list.Length + 1];
            for (int j = 0; j < i; j++)
            {
                var temp = _list[j];
                newList[j] = temp;
            }
            newList[i] = value;
            for(int j=i; j<_list.Length; j++)
            {
                var temp = _list[j];
                newList[j + 1] = temp;
            }
            _list = newList;
            Count = newList.Length;
        }

        public bool Contains(T value)
        {
            foreach (var t in _list)
            {
                if (t.Equals(value))
                    return true;
            }
            return false;
        }

        public int IndexOf(T value)
        {
            for(int i=0; i<_list.Length; i++)
            {
                if (_list[i].Equals(value))
                    return i;
            }
            return -1;
        }
        
        public T this[int i]
        {
            get
            {
                if (i >= 0 && i < Count)
                    return _list[i];
                else
                    return default(T);
            }
            set
            {
                if (i >= 0 && i < Count)
                    _list[i] = value;
            }
        }

        #region IEnumerable
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var t in _list)
            {
                yield return t;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var t in _list)
            {
                yield return t;
            }
        }
        #endregion

    }
}
