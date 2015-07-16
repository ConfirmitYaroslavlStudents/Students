using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stack;

namespace StackTests
{
    [TestClass]
    public class StackTests
    {
        [TestMethod]
        public void TestStack()
        {
            TestPushPopPeek(new Stack<int>());
        }

        [TestMethod]
        public void TestStackList()
        {
            TestPushPopPeek(new StackList<int>());
        }

        [TestMethod]
        public void TestPushPopPeek(IStack<int> stack)
        {
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            Assert.AreEqual(stack.Pop(), 4);
            Assert.AreEqual(stack.Peek(), 3);
            stack.Push(5);
            Assert.AreEqual(stack.Pop(), 5);
            Assert.AreEqual(stack.Pop(), 3);
            Assert.AreEqual(stack.Pop(), 2);
            Assert.AreEqual(stack.Pop(), 1);

        }
    }
}
