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
        public void TestMatricEquals()
        {
            var gr = new Graph<string>("A");
            gr.AddVertex("B",new HashSet<string>(new[]{"A"}));
            var matricGraph = gr.ToAdjacencyMatrixy();
            bool[,] matric = {{false, true}, {true, false}};
            bool flagEquals = true;
            for (var i = 0; i < matric.GetLength(0);++i)
            {
                for (var j = 0; j < matric.GetLength(1); j++)
                {
                    if (!matric[i, j].Equals(matricGraph[i, j]))
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


    }
}
