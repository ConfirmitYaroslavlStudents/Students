using SkillTree.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SkillTreeTest
{
    [TestClass]
    public class VertexTest
    {
        [TestMethod]
        public void CreateVertex_NameCleenCode()
        {
            var vertex = new Vertex(new Skill("CleanCode", "Hard", "imp", 10));

            Assert.AreEqual("CleanCode", vertex.Skill.Name);
        }
        [TestMethod]
        public void AddEdge_NameRefactoring()
        {
            var vertex = new Vertex(new Skill("CleanCode", "Hard", "imp", 10));
            vertex.AddEdge(new Vertex(new Skill("Refactoring", "Hard", "imp", 10)));

            Assert.AreEqual("Refactoring", vertex.Edges[0].ConnectedVertex.Skill.Name);
        }
    }

}
