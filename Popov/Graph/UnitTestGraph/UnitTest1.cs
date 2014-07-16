using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graph;
namespace UnitTestGraph
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var gr = new Graph<int>(1);
            gr.AddVertex(2,new HashSet<int>(new[]{1}));
            gr.RemoveVertex(2);
            var gr2 = new Graph <int> (1);
            Assert.AreEqual(gr.Count,gr2.Count);
        }
    }
}
