using System.Collections.Generic;
using System;

namespace SkillTree.Classes
{
    [Serializable]
    public class Discipline
    {
        public string Name {  get; set; }
        public List<Skill> Skills { get; set; }

        public Discipline() { }
        public Discipline(string name)
        {
            Name = name;
            Skills = new List<Skill>();
        }

        public bool AddSkill(Skill skill)
        {
            if (!Skills.Contains(skill))
            {
                Skills.Add(skill);
                return true;
            }
            return false;
        }
        public Skill FindSkill(string skillName)
        {
            foreach (var v in Skills)
            {
                if (v.Name.Equals(skillName))
                {
                    return v;
                }
            }
            throw new InvalidOperationException($"{skillName} not found");
        }
        public void AddRequirement(string firstSkill, string secondSkill)
        {
            var s1 = FindSkill(firstSkill);
            var s2 = FindSkill(secondSkill);
            if (s2 != null && s1 != null)
            {
                s1.AddRequirement(s2);
            }
            else
            if (s1 == null)
            {
                throw new InvalidOperationException($"{firstSkill} not found");
            }
            else
            {
                throw new InvalidOperationException($"{secondSkill} not found");
            }
        }
        public string ReturnNameAllSkills()
        {
            string toString = "";
            foreach (var skill in Skills)
            {
                if (toString == "")
                { 
                    toString = skill.Name;
                }
                else
                {
                    toString = toString + " " + skill.Name;
                }
            }
            return toString;
        }
        public List<Skill> ReturnAvailableSkills(List<Skill> learnedSkills)
        {
            var AvailableSkills = new List<Skill>();
            foreach (var сheckedSkill in Skills)
            {
                if (!learnedSkills.Contains(сheckedSkill) && CheckSkillForAvailability(сheckedSkill, learnedSkills))
                {
                    AvailableSkills.Add(сheckedSkill);
                }
            }
            return AvailableSkills;
        }
        public bool CheckSkillForAvailability(Skill сheckedskill, List<Skill> learnedSkills)
        {
            foreach(var requirement in сheckedskill.Requirements)
            {
                if(!learnedSkills.Contains(requirement.ConnectedVertex))
                {
                    return false;
                }
            }
            return true;
                

        }
    }
}
