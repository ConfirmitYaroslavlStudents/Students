using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkillTree.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SkillTreeTest
{
    [TestClass]
    public class GraphTest
    {
        private Skill Skill0 = new Skill("vertex0", "Hard", "important", 10);
        private Skill Skill1 = new Skill("vertex1", "easy", "no", 15);
        private Skill Skill2 = new Skill("vertex2", "normal", "no", 45);
        private Skill Skill3 = new Skill("vertex3", "easy", "no", 5);
        private Skill Skill4 = new Skill("vertex4", "easy", "interesting", 35);
        private Skill Skill5 = new Skill("vertex5", "hard", "no", 65);

        [TestMethod]
        public void CreateGraph_Empty()
        {
            var graph = new Graph();

            Assert.AreEqual(0, graph.Count);
        }
        [TestMethod]
        public void AddVertex_Count1()
        {
            var graph = new Graph();          
            graph.AddVertex(Skill0);

            Assert.AreEqual(1, graph.Count);
        }
        [TestMethod]
        public void AddVertex_NameVertex0()
        {
            var graph = new Graph();
            graph.AddVertex(Skill0);

            Assert.AreEqual(Skill0.Name, graph.TryGetVertex(Skill0.Name).Skill.Name);
        }
        [TestMethod]
        public void AddVertex_throwInvalidOperationException_RepeatName()
        {
            var graph = new Graph();
            var skillfirst = Skill0;
            var skillsecond = Skill0;
            graph.AddVertex(skillfirst);

            Assert.ThrowsException<InvalidOperationException>(() => graph.AddVertex(skillsecond));
        }
        [TestMethod]
        public void FindVertex_NameVertex0()
        {
            var graph = new Graph();
            graph.AddVertex(Skill0);
            
            Assert.AreEqual(Skill0.Name, graph.TryGetVertex(Skill0.Name).Skill.Name);
        }
        [TestMethod]
        public void FindVertex_throwInvalidOperationException_NotFound()
        {
            var graph = new Graph();

            Assert.ThrowsException<InvalidOperationException>(() => graph.TryGetVertex(Skill0.Name));
        }
        [TestMethod]
        public void AddEdge_NameVertex0NameVertex1()
        {
            var graph = new Graph();
            graph.AddVertex(Skill0);
            graph.AddVertex(Skill1);
            graph.AddEdge(Skill0.Name, Skill1.Name);

            Assert.AreEqual(Skill1.Name, graph.TryGetVertex(Skill0.Name).Edges[0].ConnectedVertex.Skill.Name);
        }
        [TestMethod]
        public void AddEdge_throwInvalidOperationException_NamesEqual()
        {
            var graph = new Graph();
            graph.AddVertex(Skill0);

            Assert.ThrowsException<InvalidOperationException>(() => graph.AddEdge(Skill0.Name, Skill0.Name));
        }
        [TestMethod]
        public void AddEdge_throwInvalidOperationExceptionFirst_FirstNameNotFound()
        {
            var graph = new Graph();
            graph.AddVertex(Skill1);

            Assert.ThrowsException<InvalidOperationException>(() => graph.AddEdge(Skill0.Name, Skill1.Name));
        }
        [TestMethod]
        public void AddEdge_throwInvalidOperationExceptionFirst_SecondNameNotFound()
        {
            var graph = new Graph();
            graph.AddVertex(Skill0);

            Assert.ThrowsException<InvalidOperationException>(() => graph.AddEdge(Skill0.Name, Skill1.Name));
        }
        //Graph
        //5       4
        //|\     / | 
        //| \   /  |
        //|  v v   |
        //|   0    |
        //v        v
        //2-->3--> 1
        [TestMethod]
        public void AddEdge_SerchPath_Vertex()
        {
            var graph = new Graph();
            graph.AddVertex(Skill0);
            graph.AddVertex(Skill1);
            graph.AddVertex(Skill2);
            graph.AddVertex(Skill3);
            graph.AddVertex(Skill4);
            graph.AddVertex(Skill5);
            graph.AddEdge(Skill0.Name, Skill5.Name);
            graph.AddEdge(Skill0.Name, Skill4.Name);
            graph.AddEdge(Skill1.Name, Skill4.Name);
            graph.AddEdge(Skill1.Name, Skill3.Name);
            graph.AddEdge(Skill3.Name, Skill2.Name);
            graph.AddEdge(Skill2.Name, Skill5.Name);
            var path = new List<Vertex>
            {
                graph.TryGetVertex(Skill1.Name),
                graph.TryGetVertex(Skill4.Name),
                graph.TryGetVertex(Skill3.Name),
                graph.TryGetVertex(Skill2.Name),
                graph.TryGetVertex(Skill5.Name)
            };

            CollectionAssert.AreEqual(path, graph.SearchPath(graph.TryGetVertex(Skill1.Name)));
        }
    }
}
