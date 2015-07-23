using System;
namespace Homework1
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> NextNode { get; private set; }
        public Node<T> PreviousNode { get; private set; }
	//[TODO] remove statics
        public void AddAfter(Node<T> newNode)
        {
            if (newNode == null)
                throw new ArgumentNullException("newNode");
            if (NextNode == null)
            {
                newNode.PreviousNode = this;
                NextNode = newNode;
            }
            else
            {
                newNode.NextNode = NextNode;
                newNode.PreviousNode = this;
                NextNode.PreviousNode = newNode;
                NextNode = newNode;
            }

        }
        public void AddBefore(Node<T> newNode)
        {
            if (newNode == null)
                throw new ArgumentNullException("newNode");
            if (PreviousNode == null)
            {
                newNode.NextNode = this;
                PreviousNode = newNode;
            }
            else
            {
                newNode.NextNode = this;
                newNode.PreviousNode = PreviousNode;
                PreviousNode.NextNode = newNode;
                PreviousNode = newNode;
            }

        }
        public void BreakLinks()
        {
            if (PreviousNode == null&&NextNode==null)
            {
                throw new InvalidOperationException();
            }
            if (PreviousNode == null)
            {
                NextNode.PreviousNode = null;
            }
            else if(NextNode==null)
            {
                PreviousNode.NextNode = null;
            }
            else
            {
                PreviousNode.NextNode = NextNode;
                NextNode.PreviousNode = PreviousNode;
            }
        }

    }
}
