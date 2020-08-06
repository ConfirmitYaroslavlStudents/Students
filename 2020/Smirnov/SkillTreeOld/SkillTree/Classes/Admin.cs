using System;
using System.Collections.Generic;

namespace SkillTree.Classes
{
    public class Admin: Person
    {
        public Admin(string name) : base(name)
        {
            
        }

        public bool CreateNewDiscipline(string name, List<Discipline> disciplines)
        {
            var NewDiscipline = new Discipline(name);
            if (!disciplines.Contains(NewDiscipline))
            {
                disciplines.Add(NewDiscipline);
                return true;
            }
            return false;
        }
        public bool AddScillForDiscipline(string name, Skill skill, List<Discipline> disciplines)
        {
            foreach (var discipline in disciplines)
            {
                if (discipline.Name == name)
                {
                    discipline.AddSkill(skill);
                    return true;
                }
            }          
            return true;

        }
        public bool AddRequirementForSkill(string name, string firstSkill, string secondSkill, List<Discipline> disciplines)
        {
            foreach (var discipline in disciplines)
            {
                if (discipline.Name == name)
                {
                    discipline.AddRequirement(firstSkill, secondSkill);
                    return true;
                }
            }
            return false;
        }
    }
}
