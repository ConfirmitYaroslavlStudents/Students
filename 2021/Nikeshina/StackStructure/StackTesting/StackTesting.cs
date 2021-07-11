using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackStructure;
using System.Collections.Generic;

namespace StackTesting
{
    [TestClass]
    public class StackTesting
    {
        [TestMethod]
        public void TestMethod1()
        {
            var stack = new StackStructure.Stack<int>();

            for (var i = 0; i < 100; i++)
                stack.Push(i);

            Assert.AreEqual(100, stack.Count());
        }

        [TestMethod]
        public void TestMethod2()
        {
            var stack = new StackStructure.Stack<int>();

            for (var i = 0; i < 100; i++)
            {
                stack.Push(i);
                stack.Pop();
            }

            Assert.AreEqual(0, stack.Count());
        }

        [TestMethod]
        public void TestMethod3()
        {
            var stack = new StackStructure.Stack<int>();
            var list = new List<int>();

            for (var i = 0; i < 100; i++)
            {
                stack.Push(i);
            }
            stack.Pop();

            for (var i = 98; i >-1; i--)
                Assert.AreEqual(stack.Pop(), i);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var stack = new StackStructure.Stack<int>();

            for (var i = 0; i < 100; i++)
                stack.Push(i);
            for (var i = 99; i > 50; i--)
                stack.Pop();

            Assert.AreEqual(50, stack.Peek());
        }

        [TestMethod]
        public void TestMethod5()
        {
            var stack = new StackStructure.Stack<int>();

            for (var i = 0; i < 100; i++)
                stack.Push(i);

            Assert.AreEqual(100, stack.Count());
        }
    }
}
