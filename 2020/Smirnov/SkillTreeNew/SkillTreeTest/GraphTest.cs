using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkillTree.Graph;
using System;

namespace SkillTreeTest
{
    [TestClass]
    public class GraphTest
    {
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
            var skill = new Skill("CleenCode", "Hard", "imp", 10);
            graph.AddVertex(skill);
            
            Assert.AreEqual(1, graph.Count);
        }
        [TestMethod]
        public void AddVertex_NameCleenCode()
        {
            var graph = new Graph();
            var skill = new Skill("CleenCode", "Hard", "imp", 10);
            graph.AddVertex(skill);

            Assert.AreEqual("CleenCode", graph.FindVertex("CleenCode").Skill.Name);
        }
        [TestMethod]
        public void AddVertex_throwInvalidOperationException_RepeatName()
        {
            var graph = new Graph();
            var skillfirst = new Skill("CleenCode", "Hard", "imp", 10);
            var skillsecond = new Skill("CleenCode", "easy", "noimp", 20);
            graph.AddVertex(skillfirst);

            Assert.ThrowsException<InvalidOperationException>(() => graph.AddVertex(skillsecond));
        }
        [TestMethod]
        public void FindVertex_NameRefactoring()
        {
            var graph = new Graph();
            var skillfirst = new Skill("Refactoring", "Hard", "imp", 10);
            graph.AddVertex(skillfirst);

            Assert.AreEqual("Refactoring", graph.FindVertex("Refactoring").Skill.Name);
        }
        [TestMethod]
        public void FindVertex_NotFound_ReturnNull()
        {
            var graph = new Graph();
            var skillfirst = new Skill("Refactoring", "Hard", "imp", 10);
            graph.AddVertex(skillfirst);

            Assert.IsNull(graph.FindVertex("CleenCode"));
        }
        [TestMethod]
        public void AddEdge_NameRefactoringNameCleenCode()
        {
            var graph = new Graph();
            var skillfirst = new Skill("Refactoring", "Hard", "imp", 10);
            graph.AddVertex(skillfirst);
            var skillsecond = new Skill("CleenCode", "easy", "noimp", 20);
            graph.AddVertex(skillsecond);
            graph.AddEdge("Refactoring", "CleenCode");

            Assert.AreEqual("CleenCode", graph.FindVertex("Refactoring").Edges[0].ConnectedVertex.Skill.Name);
        }
        [TestMethod]
        public void AddEdge_throwInvalidOperationException_NamesEqual()
        {
            var graph = new Graph();
            var skillfirst = new Skill("Refactoring", "Hard", "imp", 10);
            graph.AddVertex(skillfirst);
            var skillsecond = new Skill("CleenCode", "easy", "noimp", 20);
            graph.AddVertex(skillsecond);

            Assert.ThrowsException<InvalidOperationException>(() => graph.AddEdge("Refactoring", "Refactoring"));
        }
        [TestMethod]
        public void AddEdge_throwInvalidOperationExceptionFirst_FirstNameNotFound()
        {
            var graph = new Graph();
            var skillsecond = new Skill("CleenCode", "easy", "noimp", 20);
            graph.AddVertex(skillsecond);

            Assert.ThrowsException<InvalidOperationException>(() => graph.AddEdge("Refactoring", "CleenCode"));
        }
        [TestMethod]
        public void AddEdge_throwInvalidOperationExceptionFirst_SecondNameNotFound()
        {
            var graph = new Graph();
            var skillfirst = new Skill("Refactoring", "Hard", "imp", 10);
            graph.AddVertex(skillfirst);

            Assert.ThrowsException<InvalidOperationException>(() => graph.AddEdge("Refactoring", "CleenCode"));
        }
    }
}
