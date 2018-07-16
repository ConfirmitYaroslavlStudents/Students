using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionary
{
    public class Node<TKey, TValue>
    {
        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public Node<TKey, TValue> Next { get; set; }
    }

    public class MyDictionary<TKey, TValue>
    {
        Node<TKey, TValue> first;
        Node<TKey, TValue> last;
        int count;

        public MyDictionary()
        {
            first = null;
            last = null;
            count = 0;
        }

        public void Add(TKey key, TValue value)
        {
            var node = new Node<TKey, TValue>(key, value);
            if (first == null)
            {
                first = node;
            }
            else
            {
                var current = first;
                while (current != null)
                {
                    if (!current.Key.Equals(node.Key))
                    {
                        current = current.Next;
                    }
                    else
                    {
                        throw new ArgumentException("Элемент с тем же ключом уже был добавлен.");
                    }
                }
                last.Next = node;
            }
            last = node;
            count++;
        }

        public bool Remove(TKey key)
        {
            if (first.Key.Equals(key))
            {
                first = first.Next;
                count--;
                return true;
            }
            else
            {
                var current = first;
                while (current.Next != null && !current.Next.Key.Equals(key))
                {
                    current = current.Next;
                }

                if (current.Next == null)
                {
                    return false;
                }

                current.Next = current.Next.Next;
                count--;
                if (current.Next == null)
                {
                    last = current;
                }
                return true;
            }
        }

        public bool ContainsKey(TKey key)
        {
            var current = first;
            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public int Count
        {
            get { return count; }
        }

        public void Clear()
        {
            first = null;
            last = null;
            count = 0;
        }

        private Node<TKey, TValue> NodeByKey(TKey key)
        {
            var current = first;
            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    return current;
                }
                current = current.Next;
            }
            throw new KeyNotFoundException();
        }

        public TValue this[TKey key]
        {
            get { return NodeByKey(key).Value; }
            set { NodeByKey(key).Value = value; }
        }

    }
}
