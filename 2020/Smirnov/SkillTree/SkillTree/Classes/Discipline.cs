using System.Collections.Generic;
using System;

namespace SkillTree.Classes
{
    [Serializable]
    public class Discipline
    {
        public string Name { set; get; }
        public List<Skill> Skills { set; get; }

        internal Discipline() { }
        internal Discipline(string name)
        {
            Name = name;
        }
        internal Discipline(string name, List<Skill> skill)
        {
            Name = name;
            Skills = skill;
        }

        internal bool AddSkill(Skill skill)
        {
            if (!Skills.Contains(skill))
            {
                Skills.Add(skill);
                return true;
            }
            return false;
        }
        public string ReturnNameAllSkills()
        {
            string toString = "";
            foreach (var c in Skills)
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
