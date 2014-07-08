using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    public class Node<T> where T : IComparable
    {
        public T Value { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public int Height { get; private set; }

        public Node(T value)
        {
            this.Value = value;
            Left = null;
            Right = null;
            Height = 1;
        }

        public void SetHeight()
        {
            if (Left == null && Right == null)
            {
                Height = 1;
                return;
            }
            if (Left == null)
            {
                Height = Right.Height + 1;
                return;
            }
            if (Right == null)
            {
                Height = Left.Height + 1;
                return;
            }
            Height = Math.Max(Left.Height, Right.Height) + 1;
        }

        public int GetBalanceFactor()
        {
            return (Right != null ? Right.Height : 0) -
                (Left != null ? Left.Height : 0);
        }
    }
}
