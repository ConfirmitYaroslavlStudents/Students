using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SetLib;
using System.Collections.Generic;

namespace Search_Tree_Tests
{
    [TestClass]
    public class SimpleMethTests
    {
        [TestMethod]
        public void RndCountTest()
        {
            var rnd = new Random();
            var tree = new Tree<int>();
            var added = rnd.Next(100);
            var deleted = rnd.Next(added);
            var expect = added - deleted;

            for (int i = 0; i < added; i++)
            {
                tree.Add(i);
            }

            for (int i = 0; i < deleted; i++)
            {
                tree.Remove(i);
            }

            Assert.AreEqual(expect, tree.Count);
        }
    }
}
