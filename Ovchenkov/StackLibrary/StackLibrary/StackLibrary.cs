using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace StackLibrary
{
    public class Stack<T> : IEnumerable<T>
    {
        #region SubClass

        private class Element<T>
        {
            public T Value { get; set; }
            public Element<T> Next { get; set; }
        }
      
        #endregion

        #region Fields

        private Element<T> _top;
        private int _count;

        #endregion

        #region Properties

        public int Count
        {
            get { return _count; }
        }

        public T this[int i]
        {
            get
            {
                var elemnt = _top;
                for (int j = 0; j < i; ++j)
                    elemnt = elemnt.Next;
                return elemnt.Value;
            }
        }

        #endregion

        #region Constructors

        public Stack()
        {
            _top = null;
            _count = 0;
        }

        public Stack(IEnumerable<T> collection)
        {
            _top = null;
            _count = 0;
            foreach (var element in collection)
            {
                Push(element);
            }
           
        }

        #endregion

        #region Methods

        public void Push(T element)
        {

            var temp = new Element<T> {Value = element, Next = _top};
            _top = temp;
            _count++;
        }

        public T Pop()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException();
            }
            var value = _top.Value;
            _top = _top.Next;
            _count--;
            return value;
        }

        public T Peek()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException();
            }
            return _top.Value;
        }

        public void Clear()
        {
            _top = null;
            _count = 0;
        }

        public bool Contains(T element)
        {
            var stackElement = _top;
            while (stackElement != null)
            {
                if (Equals(stackElement.Value, element))
                    return true;
                stackElement = stackElement.Next;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            yield return _top.Value;

            var stackElement = _top;
            while (stackElement != null)
            {
                stackElement = stackElement.Next;
                yield return stackElement.Value;
            }
        }
      
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}


