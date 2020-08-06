using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SkillTree.Test
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void CreatePerson_NameIvan()
        {
            var user = new Classes.User("Ivan");

            Assert.AreEqual("Ivan", user.Name);
        }
        [TestMethod]
        public void LearnNewSkill_NameCleenCode()
        {
            var user = new Classes.User("Ivan");
            user.LearnNewSkill(new Classes.Skill("CleenCode", "hard", "inf", 10));
            
            Assert.AreEqual("CleenCode", user.LearnedSkills[0].Name);
        }
        [TestMethod]
        public void LearnNewSkill_InvalidOperationExceptionDontAllRequirementsMet()
        {
            var user = new Classes.User("Ivan");
            var skill = new Classes.Skill("CleenCode", "easy", "imp", 10);
            skill.AddRequirement(new Classes.Skill("Refactoring", "easy", "imp", 10));

            Assert.ThrowsException<InvalidOperationException>(() => user.LearnNewSkill(skill));
        }
        [TestMethod]
        public void LearnNewSkill_InvalidOperationExceptionRepetitiveSkill()
        {
            var user = new Classes.User("Ivan");
            var skill = new Classes.Skill("CleenCode", "easy", "imp", 10);
            user.LearnNewSkill(new Classes.Skill("CleenCode", "easy", "imp", 10));

            Assert.ThrowsException<InvalidOperationException>(() => user.LearnNewSkill(skill));
        }
        [TestMethod]
        public void ReturnAllInformationAboutSkills_CleenCodeEasyImp10()
        {
            var user = new Classes.User("Ivan");
            user.LearnNewSkill(new Classes.Skill("CleenCode", "easy", "imp", 10));
            user.ReturnAllInformationAboutSkills();

            Assert.AreEqual($"Name = \"CleenCode\" Difficult = \"easy\" Specification = \"imp\"" +
                $" Requirement = \"\" Time = \"10\"", user.ReturnAllInformationAboutSkills());
        }
        [TestMethod]
        public void ReturnNameAllSkills_CleenCode()
        {
            var user = new Classes.User("Ivan");
            user.LearnNewSkill(new Classes.Skill("CleenCode", "easy", "imp", 10));

            Assert.AreEqual("CleenCode", user.ReturnNameAllLearnedSkills());
        }
        [TestMethod]
        public void ReturnNameAllDiscipline_OOP()
        {
            var user = new Classes.User("Ivan");
            user.LearnNewSkill(new Classes.Skill("CleenCode", "easy", "imp", 10));
            user.LearnNewSkill(new Classes.Skill("Refactoring", "easy", "imp", 10));
            var discipline = new Classes.Discipline("OOP");
            discipline.AddSkill(new Classes.Skill("CleenCode", "easy", "imp", 10));
            discipline.AddSkill(new Classes.Skill("Refactoring", "easy", "imp", 10));
            user.LearnNewDiscipline(discipline);

            Assert.AreEqual("OOP", user.ReturnNameAllLearnedDisciplines());
        }
        [TestMethod]
        public void LearnNewDiscipline_OOP()
        {
            var user = new Classes.User("Ivan");
            user.LearnNewSkill(new Classes.Skill("CleenCode", "easy", "imp", 10));
            user.LearnNewSkill(new Classes.Skill("Refactoring", "easy", "imp", 10));
            var discipline = new Classes.Discipline("OOP");
            discipline.AddSkill(new Classes.Skill("CleenCode", "easy", "imp", 10));
            discipline.AddSkill(new Classes.Skill("Refactoring", "easy", "imp", 10));
            user.LearnNewDiscipline(discipline);

            Assert.AreEqual("OOP", user.LearnedDisciplines[0].Name);
        }
        [TestMethod]
        public void LearnNewDiscipline_InvalidOperationExceptionDontAllRequirementsMet()
        {
            var user = new Classes.User("Ivan");
            user.LearnNewSkill(new Classes.Skill("CleenCode", "easy", "imp", 10));
            var discipline = new Classes.Discipline("OOP");
            discipline.AddSkill(new Classes.Skill("CleenCode", "easy", "imp", 10));
            discipline.AddSkill(new Classes.Skill("Refactoring", "easy", "imp", 10));

            Assert.ThrowsException<InvalidOperationException>(() => user.LearnNewDiscipline(discipline));
        }
        [TestMethod]
        public void LearnNewDiscipline_InvalidOperationExceptionRepetitiveDiscipline()
        {
            var user = new Classes.User("Ivan");
            user.LearnNewSkill(new Classes.Skill("CleenCode", "easy", "imp", 10));
            user.LearnNewSkill(new Classes.Skill("Refactoring", "easy", "imp", 10));
            var discipline = new Classes.Discipline("OOP");
            discipline.AddSkill(new Classes.Skill("CleenCode", "easy", "imp", 10));
            discipline.AddSkill(new Classes.Skill("Refactoring", "easy", "imp", 10));
            user.LearnNewDiscipline(discipline);

            Assert.ThrowsException<InvalidOperationException>(() => user.LearnNewDiscipline(discipline));
        }

    }
}
