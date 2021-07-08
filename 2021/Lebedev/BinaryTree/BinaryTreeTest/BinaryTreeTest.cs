using Microsoft.VisualStudio.TestTools.UnitTesting;
using BinaryTree;

namespace BinaryTreeTest
{
    [TestClass]
    public class BinaryTreeTest
    {
        [TestMethod]
        public void TestRemove()
        {
            var test = new BinaryTree<int>();
            
            test.Add(23);
            test.Add(32);
            test.Add(11);
            test.Remove(23);

            Assert.IsFalse(test.Contains(23));
        }

        [TestMethod]
        public void TestContains()
        {
            BinaryTree<int> test = new BinaryTree<int>();
            
            test.Add(23);
            test.Add(32);
            test.Add(11);
            test.Add(34);
            test.Add(35);
            test.Add(33);

            Assert.IsTrue(test.Contains(33));
            Assert.IsFalse(test.Contains(12));
            Assert.IsTrue(test.Contains(11));
        }
    }
}
