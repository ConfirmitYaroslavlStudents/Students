using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;

namespace StackLibrary
{

    class EnumeratorAdapter<T> : IEnumerator<T>
    {
        IEnumerator enumerator;
        public EnumeratorAdapter(T[] array, int length)
        {
            T[] temp = new T[length];
            for (int i = 0; i < length; i++)
            {
                temp[i] = array[length - i - 1];
            }
            enumerator = temp.GetEnumerator();
        }
        public T Current
        {
            get { return (T)enumerator.Current; }
        }

        public void Dispose()
        {
            enumerator.Reset();
        }

        object System.Collections.IEnumerator.Current
        {
            get { return enumerator.Current; }
        }

        public bool MoveNext()
        {
            return enumerator.MoveNext();
        }

        public void Reset()
        {
            enumerator.Reset();
        }
    }
}
