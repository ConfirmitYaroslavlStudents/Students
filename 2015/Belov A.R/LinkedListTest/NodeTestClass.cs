using System;
using System.Text;
using Homework1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkedListTest
{
    [TestClass]
    public class NodeTestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddAfterTest()
        {
            Node<char> node = new Node<char>() {Data = 'H'};
            node.AddAfter(new Node<char>(){Data = 'o'});
            node.NextNode.AddAfter(new Node<char>() { Data = '!' });
            node.AddAfter(new Node<char>() { Data = 'l' });
            node.AddAfter(new Node<char>() { Data = 'l' });
            node.AddAfter(new Node<char>() { Data = 'e' });
            var actual=GetStringFromNode(node);
            var expected = "Hello!";
            Assert.AreEqual(expected,actual);
            new Node<char>().AddAfter(null);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddBeforeTest()
        {
            Node<char> head = new Node<char>() { Data = 'H' };
            Node<char> node = new Node<char>() { Data = '!' };
            node.AddBefore(new Node<char>() { Data = 'e' });
            node.PreviousNode.AddBefore(head);
            node.AddBefore(new Node<char>() { Data = 'l' });
            node.AddBefore(new Node<char>() { Data = 'l' });
            node.AddBefore(new Node<char>() { Data = 'o' });
            var actual = GetStringFromNode(head);
            var expected = "Hello!";
            Assert.AreEqual(expected, actual);
            new Node<char>().AddBefore(null);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveTest()
        {
            var head = new Node<char>() { Data = 'H' };
            var last=new Node<char>(){Data = '!'};
            head.AddAfter(new Node<char>() { Data = 'o' });
            head.NextNode.AddAfter(last);
            head.AddAfter(new Node<char>() { Data = 'l' });
            head.AddAfter(new Node<char>() { Data = 'l' });
            head.AddAfter(new Node<char>() { Data = 'e' });
            var node = head.NextNode.NextNode;
            head.NextNode.BreakLinks();
            head.BreakLinks();
            last.BreakLinks();
            var actual = GetStringFromNode(node);
            var expected = "llo";
            Assert.AreEqual(expected, actual);  
            var singleNode=new Node<char>();
            singleNode.BreakLinks();
        }

        string GetStringFromNode(Node<char> head)
        {
            var result = new StringBuilder();
            for (var currentNode = head; currentNode != null; currentNode = currentNode.NextNode)
            {
                result.Append(currentNode.Data);
            }
            return result.ToString();
        }
    }
}
