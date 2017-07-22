using System;

namespace SetLib
{
    internal class BinaryNode<T> where T : IComparable
    {
        public T Value { get; set; }
        public BinaryNode<T> Left { get; set; }
        public BinaryNode<T> Right { get; set; }
        public BinaryNode<T> Parent { get; set; }
        public bool IsLeaf => ChildCount == 0;
        public bool IsInternal => ChildCount > 0;
        public bool IsLeft => Parent != null && Parent.Left == this;
        public bool IsRight => Parent != null && Parent.Right == this;
        public bool HasLeft => (Left != null);
        public bool HasRight => (Right != null);
        public int ChildCount
        {
            get
            {
                int count = 0;

                if (HasLeft)
                {
                    count++;
                }
                if (HasRight)
                {
                    count++;
                }
                    
                return count;
            }
        }

        public BinaryNode(T value)
        {
            Value = value;
        }
    }
}