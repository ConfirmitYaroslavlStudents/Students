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
            var skill = new Skill("CleanCode", "Hard", "imp", 10);

            Assert.AreEqual("CleanCode", skill.Name);
        }
    }
}
