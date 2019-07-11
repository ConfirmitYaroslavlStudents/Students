namespace DaraStructures
{
    // TODO: unit tests
    // TODO: int key -> generalize
    // TODO: remove unused code
    // TODO: alignment
    // TODO: is null
    // TODO: implement IEnumerable
    public class Node<T>
    {
        public int Key { get; set; }
        public T Value { get; set; }

        public Node<T> Left { get; set; }

        public Node<T> Right { get; set; }

        public Node(int key, T value)
        {
            Key = key;
            Value = value;
        }
    }

    public class BinarySearchTree<T>
    {
        public Node<T> Root { get; set; }
        public int Count { get; private set; }

        public BinarySearchTree() { }
        public BinarySearchTree(Node<T> node)
        {
            Insert(node);
        }

        private Node<T> RecursiveDiving(Node<T> node, int key)
        {
            if (node.Key == key)
                return node;
            else if (key < node.Key)
            {
                if (node.Left is null)
                    return node;
                else
                    return RecursiveDiving(node.Left, key);
            }
            else
            {
                if (node.Right is null) return node;
                else return RecursiveDiving(node.Right, key);
            }
        }

        public void Insert(Node<T> node)
        {
            if (Root == null)
            {
                Root = node;
                Count++;
            }
            else
            {
                Node<T> current = RecursiveDiving(Root, node.Key);
                if (current.Key == node.Key) current.Value = node.Value;
                else if (node.Key < current.Key) { current.Left = node; Count++; }
                else { current.Right = node; Count++; }
            }
        }

        public Node<T> Find(int key)
        {
            if (Count == 0) return null;
            Node<T> current = RecursiveDiving(Root, key);
            if (current.Key == key) return current;
            else return null;
        }

        public void Remove(int key)
        {
            var result = RecursiveRemove(Root, key);
        }

        private Node<T> RecursiveRemove(Node<T> current, int key)
        {
            if (current is null) return current;
            if (key < current.Key)
            {
                current.Left = RecursiveRemove(current.Left, key);
            }
            else if (key > current.Key)
            {
                current.Right = RecursiveRemove(current.Right, key);
            }
            else if (current.Left != null && current.Right != null)
            {
                Node<T> find = FindMinimumNode(current.Right);
                current.Key = find.Key;
                current.Value = find.Value;
                current.Right = RecursiveRemove(current.Right, current.Key);
            }
            else
            {
                if (current.Left != null) current = current.Left;
                else current = current.Right;
                Count--;
            }
            return current;
        }

        private Node<T> FindMinimumNode(Node<T> current)
        {
            while (current.Left != null) current = current.Left;
            return current;
        }
    }
}