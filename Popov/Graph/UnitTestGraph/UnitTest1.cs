
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graph;

namespace UnitTestGraph
{
    
    static class FactoryGraph<T>
    {
        public static Graph<T> GetGraph(T vertex)
        {
            return new Graph<T>(vertex);
        }
        public static Graph<T> GetGraph()
        {
            return new Graph<T>();
        }
    }
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCountEquals()
        {
            var graph = FactoryGraph<int>.GetGraph(1);
            graph.AddVertex(2,new HashSet<int>(new[]{1}));
            graph.RemoveVertex(2);
            var graph2 = FactoryGraph<int>.GetGraph (1);
            Assert.AreEqual(graph.Count,graph2.Count);
        }

        [TestMethod]
        public void TestRemoveAdding()
        {
            var graph = FactoryGraph<int>.GetGraph(1);
            var graph2 = FactoryGraph<int>.GetGraph(1);
            graph.AddVertex(2, new HashSet<int>(new[] { 1 }));
            graph.RemoveVertex(2);
            Assert.AreEqual(graph,graph2);
        }

        [TestMethod]
        public void TestAddingIsolatedVertex()
        {
            var graph = FactoryGraph<string>.GetGraph("A");
            var graph2 = FactoryGraph<string>.GetGraph();
            graph2.AddVertex("A");           
            Assert.AreEqual(graph, graph2);
        }

        [TestMethod]
        public void TestMatrixEquals()
        {
            var graph = FactoryGraph<string>.GetGraph("A");
            graph.AddVertex("B",new HashSet<string>(new[]{"A"}));
            var matrixGraph = graph.ToAdjacencyMatrix();
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
            var graph = FactoryGraph<string>.GetGraph("A");
            graph.AddVertex("B", new HashSet<string>(new[]{"A"}));
            var count = 0;
            graph.ViewDepth("A",(s => count++));
            Assert.AreEqual(count, graph.Count);
        }

        [TestMethod]
        public void TestViewInWidth()
        {
            var graph = FactoryGraph<string>.GetGraph("A");
            graph.AddVertex("B", new HashSet<string>(new[] { "A" }));
            int count = 0;
            graph.ViewWidth("A", (s => count++));
            Assert.AreEqual(count, graph.Count);
        }

        [TestMethod]
        public void TestCountHashSet()
        {
            var graph = FactoryGraph<string>.GetGraph("A");
            graph.AddVertex("B", new HashSet<string>(new[] { "A" }));
            graph.AddVertex("C", new HashSet<string>(new[] { "A" }));
            graph.AddVertex("D", new HashSet<string>(new[] { "C" }));
            Assert.AreEqual(graph.Count, graph.SetVertex.Count);
        }

        [TestMethod]
        public void TestViewUnconnectedGraph()
        {
            var graph = FactoryGraph<string>.GetGraph("A");
            graph.AddVertex("B", new HashSet<string>(new[] { "A" }));
            graph.AddVertex("C");
            var countForViewWidth = 0;
            var countForViewDepth = 0;
            graph.ViewWidth("A", (s => countForViewWidth++));
            graph.ViewWidth("A", (s => countForViewDepth++));
            Assert.AreEqual(countForViewWidth, countForViewDepth, graph.Count);
        }
    }
}
