using System;

namespace LinkedList
{
    partial class LinkedList<T>
    {
        class LinkedListEnumerator<T> : System.Collections.Generic.IEnumerator<T>
        {
            private Node<T> _currentNode;
            private LinkedList<T> _list;

            public LinkedListEnumerator(LinkedList<T> list)
            {
                _list = list;
                _currentNode = null;
            }

            public T Current => _currentNode.Value;

            object System.Collections.IEnumerator.Current => _currentNode.Value;

            public void Dispose()
            {
                _currentNode = null;
                _list = null;
            }

            public bool MoveNext()
            {
                if (_currentNode is null)
                {
                    _currentNode = _list._head;
                    return _currentNode != null;
                }
                if (_currentNode.NextNode is null)
                    return false;
                _currentNode = _currentNode.NextNode;
                return true;
            }

            public void Reset() => _currentNode = null;
        }
    }
}
