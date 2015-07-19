using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    class LinkedListEnumerator<T>:IEnumerator<T>
    {
        private Node<T> _head;
        private Node<T> _current;

        public LinkedListEnumerator(Node<T> head)
        {
            _head = head;
            _current = null;
        }
        public T Current
        {
            get { return _current.Data; }
        }

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            if (_head == null)
            {
                return false;
            }
            if (_current == null)
            {
                _current = _head;
            }
            else
            {
                _current = _current.NextNode;
            }
            return _current != null;
        }

        public void Reset()
        {
            _current = null;
        }

        public void Dispose()
        {
        }
    }
}
