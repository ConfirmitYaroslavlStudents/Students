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
        public void AddAfterTest()
        {
            Node<char> node = new Node<char>() {Data = 'H'};
            Node<char>.AddAfter(node,new Node<char>(){Data = 'o'});
            Node<char>.AddAfter(node.NextNode, new Node<char>() { Data = '!' });
            Node<char>.AddAfter(node, new Node<char>() { Data = 'l' });
            Node<char>.AddAfter(node, new Node<char>() { Data = 'l' });
            Node<char>.AddAfter(node, new Node<char>() { Data = 'e' });
            var actual=GetStringFromNode(node);
            var expected = "Hello!";
            Assert.AreEqual(expected,actual);
            var expectedExceptions=new Exception[2];
            try
            {
                Node<char>.AddAfter(null,new Node<char>());
            }
            catch (Exception e)
            {

                expectedExceptions[0] = e;
            }
            try
            {
                Node<char>.AddAfter(new Node<char>(),null);
            }
            catch (Exception e)
            {

                expectedExceptions[1] = e;
            }
            foreach (var exception in expectedExceptions)
            {
                Assert.IsNotNull(exception);
            }
            


        }
        [TestMethod]
        public void AddBeforeTest()
        {
            Node<char> head = new Node<char>() { Data = 'H' };
            Node<char> node = new Node<char>() { Data = '!' };
            Node<char>.AddBefore(node, new Node<char>() { Data = 'e' });
            Node<char>.AddBefore(node.PreviousNode, head);
            Node<char>.AddBefore(node, new Node<char>() { Data = 'l' });
            Node<char>.AddBefore(node, new Node<char>() { Data = 'l' });
            Node<char>.AddBefore(node, new Node<char>() { Data = 'o' });
            var actual = GetStringFromNode(head);
            var expected = "Hello!";
            Assert.AreEqual(expected, actual);
            var expectedExceptions = new Exception[2];
            try
            {
                Node<char>.AddBefore(null, new Node<char>());
            }
            catch (Exception e)
            {

                expectedExceptions[0] = e;
            }
            try
            {
                Node<char>.AddBefore(new Node<char>(), null);
            }
            catch (Exception e)
            {

                expectedExceptions[1] = e;
            }
            foreach (var exception in expectedExceptions)
            {
                Assert.IsNotNull(exception);
            }



        }
        [TestMethod]
        public void RemoveTest()
        {
            var head = new Node<char>() { Data = 'H' };
            var last=new Node<char>(){Data = '!'};
            Node<char>.AddAfter(head, new Node<char>() { Data = 'o' });
            Node<char>.AddAfter(head.NextNode, last);
            Node<char>.AddAfter(head, new Node<char>() { Data = 'l' });
            Node<char>.AddAfter(head, new Node<char>() { Data = 'l' });
            Node<char>.AddAfter(head, new Node<char>() { Data = 'e' });
            var node = head.NextNode.NextNode;
            Node<char>.Remove(head.NextNode);
            Node<char>.Remove(head);
            Node<char>.Remove(last);
            var actual = GetStringFromNode(node);
            var expected = "llo";
            Assert.AreEqual(expected, actual);
            var expectedExceptions = new Exception[2];
            try
            {
                var singleNode=new Node<char>();
                Node<char>.Remove(singleNode);
            }
            catch (Exception e)
            {

                expectedExceptions[0] = e;
            }
            try
            {
                Node<char>.Remove(null);
            }
            catch (Exception e)
            {

                expectedExceptions[1] = e;
            }
            foreach (var exception in expectedExceptions)
            {
                Assert.IsNotNull(exception);
            }



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
