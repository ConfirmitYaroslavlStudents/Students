using System;
using System.ComponentModel;

namespace Homework1
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> NextNode { get; private set; }
        public Node<T> PreviousNode { get; private set; }
	//[TODO] remove statics
        public static void AddAfter(Node<T> node, Node<T> newNode)
        {
            if (node == null)
                throw new ArgumentNullException("node");
            if (newNode == null)
                throw new ArgumentNullException("newNode");
            if (node.NextNode == null)
            {
                newNode.PreviousNode = node;
                node.NextNode = newNode;
            }
            else
            {
                newNode.NextNode = node.NextNode;
                newNode.PreviousNode = node;
                node.NextNode.PreviousNode = newNode;
                node.NextNode = newNode;
            }

        }
        public static void AddBefore(Node<T> node, Node<T> newNode)
        {
            if (node == null)
                throw new ArgumentNullException("node");
            if (newNode == null)
                throw new ArgumentNullException("newNode");
            if (node.PreviousNode == null)
            {
                newNode.NextNode = node;
                node.PreviousNode = newNode;
            }
            else
            {
                newNode.NextNode = node;
                newNode.PreviousNode = node.PreviousNode;
                node.PreviousNode.NextNode = newNode;
                node.PreviousNode = newNode;
            }

        }

        public static void Remove(Node<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            if (node.PreviousNode == null&&node.NextNode==null)
            {
                throw new InvalidOperationException();
            }
            if (node.PreviousNode == null)
            {
                node.NextNode.PreviousNode = null;
            }
            else if(node.NextNode==null)
            {
                node.PreviousNode.NextNode = null;
            }
            else
            {
                node.PreviousNode.NextNode = node.NextNode;
                node.NextNode.PreviousNode = node.PreviousNode;
            }
        }

    }
}
