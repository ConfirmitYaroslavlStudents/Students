using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace StackWithUseArray
{
    class Adaptee<T>
    {
        public IEnumerator<T> GetEnumerator(T[] _array,int length)
        {
            return new ArrayEnumerator(_array, length);
        }
        public class ArrayEnumerator:IEnumerator<T>, IDisposable, IEnumerator
        {
            private int length;
            private int _index = -1;
            private T[] _array;

              public ArrayEnumerator(T[] _array,int length)
            {
                this._array = _array;
                this._index = length;
                this.length = length;
            }

            public T Current
            {
                get
                {
                    if (_index == _array.Length)

                        throw new InvalidOperationException("MyStack is Empty");

                    else

                        return _array[_index];

                }
            }

            object IEnumerator.Current
            {
                get
                {
                    if (_index == _array.Length)

                        throw new InvalidOperationException("MyStack is Empty");

                    else

                        return (object)_array[_index];;


                }
            }

            public void Dispose()
            {
                this._index = _array.Length;
            }

            void IEnumerator.Reset()
            {
                this._index = -1;
                
            }

            public bool MoveNext()
            {
                --this._index;
                bool canMove = this._index >= 0 && this._index <= length;
                return canMove;

            }

        }

    }
}

