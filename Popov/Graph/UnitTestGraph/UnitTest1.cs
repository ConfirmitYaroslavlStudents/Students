
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graph;

namespace UnitTestGraph
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCountEquals()
        {
            var gr = new Graph<int>(1);
            gr.AddVertex(2,new HashSet<int>(new[]{1}));
            gr.RemoveVertex(2);
            var gr2 = new Graph <int> (1);
            Assert.AreEqual(gr.Count,gr2.Count);
        }

        [TestMethod]
        public void TestRemoveAdding()
        {
            var gr = new Graph<int>(1);
            var gr2 = new Graph<int>(1);
            gr.AddVertex(2, new HashSet<int>(new[] { 1 }));
            gr.RemoveVertex(2);
            Assert.AreEqual(gr,gr2);
        }

        [TestMethod]
        public void TestAddingIsolatedVertex()
        {
            var gr = new Graph<string>("A");
            var gr2 = new Graph<string>();
            gr2.AddVertex("A");           
            Assert.AreEqual(gr, gr2);
        }

        [TestMethod]
        public void TestMatrixEquals()
        {
            var gr = new Graph<string>("A");
            gr.AddVertex("B",new HashSet<string>(new[]{"A"}));
            var matrixGraph = gr.ToAdjacencyMatrix();
            bool[,] matrix = {{false, true}, {true, false}};
            bool flagEquals = true;
            for (var i = 0; i < matrix.GetLength(0);++i)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    if (!matrix[i, j].Equals(matrixGraph[i, j]))
                    {
                        flagEquals = false;
                    }
                    
                }
            }
            Assert.AreEqual(flagEquals,true);
            
        }

        [TestMethod]
        public void TestCountConsoleMenu()
        {
            var menu = new ConsoleMenu();
            var i = 0;
            menu.AddCommand("Nothing",()=>i=1);
            Assert.AreEqual(menu.Count, 1);
        }

        [TestMethod]
        public void TestViewInDepth()
        {
            var gr = new Graph<string>("A");
            gr.AddVertex("B", new HashSet<string>(new[]{"A"}));
            var count = 0;
            gr.ViewDepth("A",(s => count++));
            Assert.AreEqual(count, gr.Count);
        }

        [TestMethod]
        public void TestViewInWidth()
        {
            var gr = new Graph<string>("A");
            gr.AddVertex("B", new HashSet<string>(new[] { "A" }));
            int count = 0;
            gr.ViewWidth("A", (s => count++));
            Assert.AreEqual(count, gr.Count);
        }

        [TestMethod]
        public void TestCountHashSet()
        {
            var gr = new Graph<string>("A");
            gr.AddVertex("B", new HashSet<string>(new[] { "A" }));
            gr.AddVertex("C", new HashSet<string>(new[] { "A" }));
            gr.AddVertex("D", new HashSet<string>(new[] { "C" }));
            Assert.AreEqual(gr.Count, gr.SetVertex.Count);
        }

        [TestMethod]
        public void TestViewUnconnectedGraph()
        {
            var gr = new Graph<string>("A");
            gr.AddVertex("B", new HashSet<string>(new[] { "A" }));
            gr.AddVertex("C");
            int countForViewWidth = 0;
            int countForViewDepth = 0;
            gr.ViewWidth("A", (s => countForViewWidth++));
            gr.ViewWidth("A", (s => countForViewDepth++));
            Assert.AreEqual(countForViewWidth, countForViewDepth, gr.Count);
        }
    }
}
