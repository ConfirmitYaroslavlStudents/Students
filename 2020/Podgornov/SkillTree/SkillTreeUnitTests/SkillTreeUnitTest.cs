using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkillTree;

namespace SkillTreeUnitTests
{
    [TestClass]
    public class SkillTreeUnitTest
    {
        [TestMethod]
        public void CreateGraph_InitializationWorkCorrectly()
        {
            var graph = new Graph();
            Assert.AreEqual(graph.MaxId, 0);
        }

        [TestMethod]
        public void AddSkillWorkCorrectly()
        {
            var graph = new Graph();
            var inputSkill = new Skill("skill1", "10h", SkillComplexity.Easy);
            graph.AddVertex(inputSkill);
            Assert.IsTrue(graph[0].Available);
            Assert.IsFalse(graph[0].Finish);
            Assert.AreEqual(inputSkill, graph[0].Skill);
        }

        [TestMethod]
        public void RemoveSkillWorkCorrectly()
        {
            var graph = new Graph();
            var inputSkill = new Skill("skill1", "10h", SkillComplexity.Easy);
            graph.AddVertex(inputSkill);
            graph.RemoveVertex(0);
            Assert.AreEqual(0, graph.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(GraphException))]
        public void Graph_throwGraphEx_When_Add_Dependence_On_Yourself()
        {
            var graph = new Graph();
            graph.AddVertex(new Skill("skill1", "10h", SkillComplexity.Easy));
            graph.AddDependence(0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(GraphException))]
        public void AddDependence_Graph_throwGraphEx_When_Circle_Was_Created()
        {
            var graph = new Graph();
            graph.AddVertex(new Skill("skill1", "10h", SkillComplexity.Easy));
            graph.AddVertex(new Skill("skill2", "11h", SkillComplexity.Middle));
            graph.AddDependence(0, 1);
            graph.AddDependence(1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(GraphException))]
        public void AddDependence_Graph_throwGraphEx_When_Dependence_Exist()
        {
            var graph = new Graph();
            graph.AddVertex(new Skill("skill1", "10h", SkillComplexity.Easy));
            graph.AddVertex(new Skill("skill2", "11h", SkillComplexity.Middle));
            graph.AddDependence(0, 1);
            graph.AddDependence(0, 1);
        }

        [TestMethod]
        public void AddDependenceWorkCorrectly()
        {
            var graph = new Graph();
            graph.AddVertex(new Skill("skill1", "10h", SkillComplexity.Easy));
            graph.AddVertex(new Skill("skill2", "11h", SkillComplexity.Middle));
            Assert.IsTrue(graph[1].Available);
            graph.AddDependence(0, 1);
            Assert.IsFalse(graph[1].Available);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveDependence_throw_InvOprEx_When_No_Dependency()
        {
            var graph = new Graph();
            graph.AddVertex(new Skill("skill1", "10h", SkillComplexity.Easy));
            graph.AddVertex(new Skill("skill2", "11h", SkillComplexity.Middle));
            graph.AddDependence(0, 1);
            graph.RemoveDependence(1, 0);
        }

        [TestMethod]
        public void RemoveDependenceWorkCorrectly()
        {
            var graph = new Graph();
            graph.AddVertex(new Skill("skill1", "10h", SkillComplexity.Easy));
            graph.AddVertex(new Skill("skill2", "11h", SkillComplexity.Middle));
            Assert.IsTrue(graph[1].Available);
            graph.AddDependence(0, 1);
            Assert.IsFalse(graph[1].Available);
            graph.RemoveDependence(0, 1);
            Assert.IsTrue(graph[1].Available);
        }

        [TestMethod]
        public void RecognizeWorkCorrectly()
        {
            var graph = new Graph();
            graph.AddVertex(new Skill("skill1", "10h", SkillComplexity.Easy));
            Assert.IsFalse(graph[0].Finish);
            graph[0].Recognize();
            Assert.IsTrue(graph[0].Finish);
        }

        [TestMethod]
        public void Skills__Open_when_All_Dependencies_Recognize()
        {
            var graph = new Graph();
            graph.AddVertex(new Skill("skill1", "10h", SkillComplexity.Easy));
            graph.AddVertex(new Skill("skill2", "11h", SkillComplexity.Middle));
            graph.AddVertex(new Skill("skill3", "11h", SkillComplexity.Hard));
            graph.AddDependence(0, 2);
            graph.AddDependence(1, 2);
            Assert.IsFalse(graph[2].Available);
            graph[0].Recognize();
            Assert.IsFalse(graph[2].Available);
            graph[1].Recognize();
            Assert.IsTrue(graph[1].Available);
        }

    }
}
