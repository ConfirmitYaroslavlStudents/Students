using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SkillTree.Test
{
    [TestClass]
    public class DisciplineTest
    {
        [TestMethod]
        public void CreateDiscipline_NameOOP()
        {
            var discipline = new Classes.Discipline("OOP");

            Assert.AreEqual("OOP", discipline.Name);
        }
        [TestMethod]
        public void AddSkill_NamePolymorphism()
        {
            var discipline = new Classes.Discipline("OOP");
            discipline.AddSkill(new Classes.Skill("Polymorphism", "easy", "important", 15));

            Assert.AreEqual("Polymorphism", discipline.Skills[0].Name);
        }
        [TestMethod]
        public void FindSkill_NamePolymorphism()
        {
            var discipline = new Classes.Discipline("OOP");
            discipline.AddSkill(new Classes.Skill("Polymorphism", "easy", "important", 15));

            Assert.AreEqual("Polymorphism", discipline.FindSkill("Polymorphism").Name);
        }
        [TestMethod]
        public void FindSkill_throwInvalidOperationException()
        {
            var discipline = new Classes.Discipline("OOP");

            Assert.ThrowsException<InvalidOperationException>(() => discipline.FindSkill("Polymorphism"));
        }
        [TestMethod]
        public void AddRequirement_NamePolymorphismAndNameCleenCode()
        {
            var discipline = new Classes.Discipline("OOP");
            discipline.AddSkill(new Classes.Skill("Polymorphism", "easy", "important", 15));
            discipline.AddSkill(new Classes.Skill("CleenCode", "easy", "important", 15));
            discipline.AddRequirement("Polymorphism", "CleenCode");

            Assert.AreEqual("CleenCode", discipline.FindSkill("Polymorphism").Requirements[0].ConnectedVertex.Name);
        }
        [TestMethod]
        public void AddRequirement_throwInvalidOperationExceptionNotFoundFerstSkill()
        {
            var discipline = new Classes.Discipline("OOP");
            discipline.AddSkill(new Classes.Skill("CleenCode", "easy", "important", 15));

            Assert.ThrowsException<InvalidOperationException>(() => discipline.AddRequirement("Polymorphism", "CleenCode"));
        }

        [TestMethod]
        public void AddRequirement_throwInvalidOperationExceptionNotFoundSecondSkill()
        {
            var discipline = new Classes.Discipline("OOP");
            discipline.AddSkill(new Classes.Skill("Polymorphism", "easy", "important", 15));

            Assert.ThrowsException<InvalidOperationException>(() => discipline.AddRequirement("Polymorphism", "CleenCode"));
        }
        [TestMethod]
        public void ReturnNameAllSkills_PolymorphismCleenCode()
        {
            var discipline = new Classes.Discipline("OOP");
            discipline.AddSkill(new Classes.Skill("Polymorphism", "easy", "important", 15));
            discipline.AddSkill(new Classes.Skill("CleenCode", "easy", "important", 15));

            Assert.AreEqual("Polymorphism CleenCode", discipline.ReturnNameAllSkills());
        }
        [TestMethod]
        public void ReturnAvailableSkills_NameCleenCode()
        {
            var discipline = new Classes.Discipline("OOP");
            discipline.AddSkill(new Classes.Skill("Polymorphism", "easy", "important", 15));
            discipline.AddSkill(new Classes.Skill("CleenCode", "easy", "important", 15));
            var skills = new List<Classes.Skill>();
            skills.Add(new Classes.Skill("Polymorphism", "easy", "important", 15));
            

            Assert.IsTrue(discipline.ReturnAvailableSkills(skills)[0].Equals(new Classes.Skill("CleenCode", "easy", "important", 15)));
        }
        [TestMethod]
        public void CheckSkillForAvailability_True()
        {
            var discipline = new Classes.Discipline("OOP");
            discipline.AddSkill(new Classes.Skill("Polymorphism", "easy", "important", 15));
            discipline.AddSkill(new Classes.Skill("CleenCode", "easy", "important", 15));
            var skills = new List<Classes.Skill>();
            skills.Add(new Classes.Skill("Polymorphism", "easy", "important", 15));


            Assert.IsTrue(discipline.CheckSkillForAvailability(new Classes.Skill("CleenCode", "easy", "important", 15), skills));
        }
        [TestMethod]
        public void CheckSkillForAvailability_False()
        {
            var discipline = new Classes.Discipline("OOP");
            discipline.AddSkill(new Classes.Skill("Polymorphism", "easy", "important", 15));
            discipline.AddSkill(new Classes.Skill("Refactoring", "easy", "important", 15));
            discipline.AddSkill(new Classes.Skill("CleenCode", "easy", "important", 15));
            discipline.AddRequirement("CleenCode", "Refactoring");
            var skills = new List<Classes.Skill>();
            skills.Add(new Classes.Skill("Polymorphism", "easy", "important", 15));


            Assert.IsFalse(discipline.CheckSkillForAvailability(discipline.FindSkill("CleenCode"), skills));
        }
    }
}
