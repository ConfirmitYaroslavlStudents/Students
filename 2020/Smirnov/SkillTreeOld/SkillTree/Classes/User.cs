using System;
using System.Collections.Generic;

namespace SkillTree.Classes
{
    [Serializable]
    public class User: Person
    {
        public List<Skill> LearnedSkills { get; set; }
        public List<Discipline> LearnedDisciplines { get; set; }

        public User() { }
        public User(string name) : base (name)
        {
            LearnedSkills = new List<Skill>();
            LearnedDisciplines = new List<Discipline>();
        }

        public void LearnNewSkill(Skill skill)
        {
            if (LearnedSkills.Contains(skill))
            {
                throw new InvalidOperationException("Skill has already been learned");
            }
            if (skill.Requirements.Count == 0)
            {
                LearnedSkills.Add(skill);
                return;
            }
            foreach (var requirement in skill.Requirements)
            {
                if (!LearnedSkills.Contains(requirement.ConnectedVertex))
                {
                    throw new InvalidOperationException("Not all requirements are met");
                }
            }
            LearnedSkills.Add(skill);
        }
        public void LearnNewDiscipline(Discipline discipline)
        {
            if (LearnedDisciplines.Contains(discipline))
            {
                throw new InvalidOperationException("Discipline has already been learned");
            }
            foreach(var skill in discipline.Skills)
            {
                if(!LearnedSkills.Contains(skill))
                {
                    throw new InvalidOperationException("Not all skills are learned");
                }
            }
            LearnedDisciplines.Add(discipline);
        }
        public string ReturnAllInformationAboutSkills()
        {
            string toString = "";
            foreach (var c in LearnedSkills)
            {
                toString += c.ToString();
            }
            return toString;
        }
        public string ReturnNameAllLearnedSkills()
        {
            string toString = "";
            foreach (var c in LearnedSkills)
            {
                if (toString == "")
                {
                    toString = c.Name;
                }
                else
                {
                    toString = toString + " " + c.Name;
                }
            }
            return toString;
        }
        public string ReturnNameAllLearnedDisciplines()
        {
            string toString = "";
            foreach (var c in LearnedDisciplines)
            {
                if (toString == "")
                {
                    toString = c.Name;
                }
                else
                {
                    toString = toString + " " + c.Name;
                }
            }
            return toString;
        }


    }
}
