using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace myList
{
   public class MyList<T> : IEnumerable<T>
   {
        private T[] _contents = new T[3]; 
        private int _count;

       public MyList()
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

       private void Expand()
       {
           T[] temp = _contents;
           _contents = new T[Count * 2];
           temp.CopyTo(_contents, 0);
       }

        public void Clear()
        {
            _count = 0;
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
            if ( (index < Count) && (index >= 0))
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
            for (int i = 0; i < Count; i++)
            {
                yield return _contents[i];
            }         
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

       //for debug purposes
        public void PrintContents()
        {
            Console.WriteLine("List has a capacity of {0} and currently has {1} elements.", _contents.Length, _count);
            Console.Write("List contents:");
            for (int i = 0; i < Count; i++)
            {
                Console.Write(" {0}", _contents[i]);
            }
            Console.WriteLine();
        }
       
    }
}
