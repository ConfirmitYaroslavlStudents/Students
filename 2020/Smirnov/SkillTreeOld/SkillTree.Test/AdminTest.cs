using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SkillTree.Test
{
    [TestClass]
    public class AdminTest
    {
        [TestMethod]
        public void CreateNewAdmin_NameIvan()
        {
            var admin = new Classes.Admin("Ivan");

            Assert.AreEqual("Ivan", admin.Name);
        }
        [TestMethod]
        public void CreateNewDiscipline_NameProgramming()
        {
            var admin = new Classes.Admin("Ivan");
            var disciplines = new List<Classes.Discipline>();
            admin.CreateNewDiscipline("Programming", disciplines);

            Assert.AreEqual("Programming", disciplines[0].Name);
        }

        [TestMethod]
        public void AddScillForDiscipline_NamePolymorphismDisciplineOOP()
        {
            var admin = new Classes.Admin("Ivan");
            var disciplines = new List<Classes.Discipline>();
            admin.CreateNewDiscipline("OOP", disciplines);
            admin.AddScillForDiscipline("OOP", new Classes.Skill("Polymorphism", "easy", "important", 15), disciplines);

            var expectedSkill = new Classes.Skill("Polymorphism", "easy", "important", 15);

            Assert.AreEqual("OOP", disciplines[0].Name);
            Assert.IsTrue(expectedSkill.Equals(disciplines[0].Skills[0]));

        }
        [TestMethod]
        public void AddRequirementForSkill_NamePolymorphismAndNameCleenCode()
        {
            var admin = new Classes.Admin("Ivan");
            var disciplines = new List<Classes.Discipline>();
            admin.CreateNewDiscipline("OOP", disciplines);
            admin.AddScillForDiscipline("OOP", new Classes.Skill("Polymorphism", "easy", "important", 15), disciplines);
            admin.AddScillForDiscipline("OOP", new Classes.Skill("Cleen Code", "easy", "important", 15), disciplines);
            admin.AddRequirementForSkill("OOP", "Polymorphism", "Cleen Code", disciplines);

            Assert.AreEqual("Cleen Code", disciplines[0].Skills[0].Requirements[0].ConnectedVertex.Name);

        }
    }
}
