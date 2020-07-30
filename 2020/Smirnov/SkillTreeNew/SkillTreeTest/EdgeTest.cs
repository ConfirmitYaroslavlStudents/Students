using SkillTree.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SkillTreeTest
{
    [TestClass]
    public class EdgeTest
    {
        [TestMethod]
        public void CreateEdge_ConnectedVertex_NameCleenCode()
        {
            var edge = new Edge(new Vertex(new Skill("CleenCode", "Hard", "imp", 10)));

            Assert.AreEqual("CleenCode", edge.ConnectedVertex.Skill.Name);
        }
    }
}
