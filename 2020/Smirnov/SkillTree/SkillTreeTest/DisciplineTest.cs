using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkillTree;

namespace SkillTreeTest
{
    [TestClass]
    public class DisciplineTest
    {
        [TestMethod]
        public void CreateDiscipline_NameOOP()
        {
            var discipline = new Discipline("OOP");

            Assert.AreEqual("OOP", discipline.Name);
        }
    }
}
