using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SkillTree.Test
{
    [TestClass]
    public class SkillTest
    {
        [TestMethod]
        public void CreateSkill_NameInheritance()
        {
            var skill = new Classes.Skill("Inheritance", "Easy", "Important", 20);
            Assert.AreEqual("Inheritance", skill.Name);
            Assert.AreEqual("Easy", skill.Difficult);
            Assert.AreEqual("Important", skill.Specification);
            CollectionAssert.AreEqual(new List<Classes.Skill>(), skill.Requirements);
            Assert.AreEqual(20, skill.Time);

        }
        [TestMethod]
        public void ToString_NameInheritance()
        {
            var skill = new Classes.Skill("Inheritance", "Easy", "Important", 20);
            string expectedString = "Name = \"Inheritance\" Difficult = \"Easy\" Specification = \"Important\"" +
                $" Requirement = \"\" Time = \"20\"";
            Assert.AreEqual(expectedString, skill.ToString());

        }
        [TestMethod]
        public void ReturnNameRequirements_NameCleenCode()
        {
            var discipline = new Classes.Discipline("OOP");
            var skill = new Classes.Skill("Inheritance", "Easy", "Important", 20);
            discipline.AddSkill(skill);
            var requirementSkill = new Classes.Skill("Cleen Code", "hard", "important", 77);
            discipline.AddSkill(requirementSkill);

            discipline.AddRequirement("Inheritance", "Cleen Code");
            
            Assert.AreEqual("Cleen Code", skill.ReturnNameRequirements());

        }
        [TestMethod]
        public void Equals_true()
        {
            var skillFerst = new Classes.Skill("Inheritance", "Easy", "Important", 20);
            var skillSecond = new Classes.Skill("Inheritance", "Easy", "Important", 20);

            Assert.IsTrue(skillFerst.Equals(skillSecond));

        }
        [TestMethod]
        public void Equals_flase()
        {
            var skillFerst = new Classes.Skill("Inheritance", "Easy", "Important", 20);
            var skillSecond = new Classes.Skill("Polymorphism", "Easy", "Important", 20);

            Assert.IsFalse(skillFerst.Equals(skillSecond));

        }
        [TestMethod]
        public void AddRequirementWorkCorrectly()
        {
            var skillFerst = new Classes.Skill("Inheritance", "Easy", "Important", 20);
            var skillSecond = new Classes.Skill("Polymorphism", "Easy", "Important", 20);

            skillFerst.AddRequirement(skillSecond);

            Assert.AreEqual("Polymorphism", skillFerst.Requirements[0].ConnectedVertex.Name);

        }

    }
}
