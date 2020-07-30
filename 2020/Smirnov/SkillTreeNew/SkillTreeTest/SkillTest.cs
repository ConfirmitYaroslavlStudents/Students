using SkillTree.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SkillTreeTest
{
    [TestClass]
    public class SkillTest
    {
        [TestMethod]
        public void CreateSkill_NameCleenCode()
        {
            var skill = new Skill("CleenCode", "Hard", "imp", 10);

            Assert.AreEqual("CleenCode", skill.Name);
        }
    }
}
