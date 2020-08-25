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
            var graph = new Graph<Skill>();
            Assert.AreEqual(graph.MaxId, 0);
        }

        [TestMethod]
        public void AddSkillWorkCorrectly()
        {
            var graph = new Graph<Skill>();
            var inputSkill = new Skill("skill1", 10, SkillComplexity.Easy);
            var vertex = graph.AddVertex(inputSkill);
            var gStatus = new GraphStatus<Skill>(graph);
            Assert.IsTrue(gStatus.IsVertexAvailable(vertex));
            Assert.IsFalse(gStatus.IsVertexFinished(vertex));
            Assert.AreEqual(inputSkill, graph.GetVertexById(0).Value);
        }

        [TestMethod]
        public void RemoveSkillWorkCorrectly()
        {
            var graph = new Graph<Skill>();
            var inputSkill = new Skill("skill1", 10, SkillComplexity.Easy);
            graph.AddVertex(inputSkill);
            graph.RemoveVertex(0);
            Assert.AreEqual(0, graph.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(GraphException))]
        public void Graph_throwGraphEx_When_Add_Dependence_On_Yourself()
        {
            var graph = new Graph<Skill>();
            var vertex = graph.AddVertex(new Skill("skill1", 10, SkillComplexity.Easy));
            graph.AddDependence(vertex, vertex);
        }

        [TestMethod]
        [ExpectedException(typeof(GraphException))]
        public void AddDependence_Graph_throwGraphEx_When_Circle_Was_Created()
        {
            var graph = new Graph<Skill>();
            var vertex0 = graph.AddVertex(new Skill("skill1", 10, SkillComplexity.Easy));
            var vertex1 = graph.AddVertex(new Skill("skill2", 10, SkillComplexity.Middle));
            graph.AddDependence(vertex0, vertex1);
            graph.AddDependence(vertex1, vertex0);
        }

        [TestMethod]
        [ExpectedException(typeof(GraphException))]
        public void AddDependence_Graph_ThrowGraphEx_When_Dependence_Exist()
        {
            var graph = new Graph<Skill>();
            var vertex0 = graph.AddVertex(new Skill("skill1", 10, SkillComplexity.Easy));
            var vertex1 = graph.AddVertex(new Skill("skill2", 10, SkillComplexity.Middle));
            graph.AddDependence(vertex0, vertex1);
            graph.AddDependence(vertex0, vertex1);
        }

        [TestMethod]
        public void AddDependenceWorkCorrectly()
        {
            var graph = new Graph<Skill>();
            var vertex0 = graph.AddVertex(new Skill("skill1", 10, SkillComplexity.Easy));
            var vertex1 = graph.AddVertex(new Skill("skill2", 10, SkillComplexity.Middle));
            var gStatus1 = new GraphStatus<Skill>(graph);
            Assert.IsTrue(gStatus1.IsVertexAvailable(vertex1));
            graph.AddDependence(vertex0, vertex1);
            var gStatus2 = new GraphStatus<Skill>(graph);
            Assert.IsFalse(gStatus2.IsVertexAvailable(vertex1));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveDependence_Throw_InvOprEx_When_No_Dependency()
        {
            var graph = new Graph<Skill>();
            var vertex0 = graph.AddVertex(new Skill("skill1", 10, SkillComplexity.Easy));
            var vertex1 = graph.AddVertex(new Skill("skill2", 10, SkillComplexity.Middle));
            graph.AddDependence(vertex0, vertex1);
            graph.RemoveDependence(vertex1, vertex0);
        }

        [TestMethod]
        public void RecognizeWorkCorrectly()
        {
            var graph = new Graph<Skill>();
            var vertex = graph.AddVertex(new Skill("skill1", 10, SkillComplexity.Easy));
            var gStatus = new GraphStatus<Skill>(graph);
            Assert.IsFalse(gStatus.IsVertexFinished(vertex));
            gStatus.FinishVertex(vertex);
            Assert.IsTrue(gStatus.IsVertexFinished(vertex));
        }

        [TestMethod]
        public void Skills_Open_When_All_Dependencies_Recognize()
        {
            var graph = new Graph<Skill>();
            var vertex0 = graph.AddVertex(new Skill("skill1", 10, SkillComplexity.Easy));
            var vertex1 = graph.AddVertex(new Skill("skill2", 10, SkillComplexity.Middle));
            var vertex2 = graph.AddVertex(new Skill("skill3", 10, SkillComplexity.Hard));
            graph.AddDependence(vertex0, vertex2);
            graph.AddDependence(vertex1, vertex2);
            var gStatus = new GraphStatus<Skill>(graph);
            Assert.IsFalse(gStatus.IsVertexAvailable(vertex2));
            gStatus.FinishVertex(vertex0);
            Assert.IsFalse(gStatus.IsVertexAvailable(vertex2));
            gStatus.FinishVertex(vertex1);
            Assert.IsTrue(gStatus.IsVertexAvailable(vertex2));
        }

        [TestMethod]
        public void Dependencies_Return_Correctly()
        {
            /*   4     1
             *  / \   ^ \ 
             * v   v /   v
             *6     3     0
             * \   ^ \   ^
             *  v /   v / 
             *   5     2
             */
            var graph = new Graph<Skill>();
            var vertex0 = graph.AddVertex(new Skill("skill0", 10, SkillComplexity.Easy));
            var vertex1 = graph.AddVertex(new Skill("skill1", 10, SkillComplexity.Easy));
            var vertex2 = graph.AddVertex(new Skill("skill2", 10, SkillComplexity.Middle));
            var vertex3 = graph.AddVertex(new Skill("skill3", 10, SkillComplexity.Hard));
            var vertex4 = graph.AddVertex(new Skill("skill4", 10, SkillComplexity.Hard));
            var vertex5 = graph.AddVertex(new Skill("skill4", 10, SkillComplexity.Hard));
            var vertex6 = graph.AddVertex(new Skill("skill6", 10, SkillComplexity.Hard));
            graph.AddDependence(vertex1, vertex0);
            graph.AddDependence(vertex2, vertex0);
            graph.AddDependence(vertex3, vertex1);
            graph.AddDependence(vertex3, vertex2);
            graph.AddDependence(vertex4, vertex3);
            graph.AddDependence(vertex5, vertex3);
            graph.AddDependence(vertex4, vertex6);
            graph.AddDependence(vertex6, vertex5);

            var real = graph.GetAllDependencies(vertex0).Select(i => i.Id).ToArray();
            CollectionAssert.AreEqual(new int[] { 4, 6, 5, 3, 2, 1 }, real);
        }
    }
}
