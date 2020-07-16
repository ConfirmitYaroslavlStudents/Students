using System;
using System.Collections.Generic;

namespace SkillTree.Classes
{
    [Serializable]
    public class User: Person
    {
        private List<Skill> _availableSkills;

        public User() { }
        public User(string name) : base (name)
        {
            _availableSkills = new List<Skill>();
        }
        public bool LearnNewSkill(Skill skill)
        {
            if (!_availableSkills.Contains(skill))
            {
                _availableSkills.Add(skill);
                return true;
            }
            return false;
        }
        public string ReturnAllInformationAboutSkills()
        {
            string toString = "";
            foreach (var c in _availableSkills)
            {
                toString += c.ToString();
            }
            return toString;
        }
        public string ReturnNameAllSkills()
        {
            string toString = "";
            foreach (var c in _availableSkills)
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
