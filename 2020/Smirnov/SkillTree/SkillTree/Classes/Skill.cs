using System.Collections.Generic;
using System;
namespace SkillTree.Classes
{
    [Serializable]
    public class Skill
    {
        public string Name { set; get; }
        public string Difficult { set; get; }
        public string Specification { set; get; }
        public List<Skill> Requirement { set; get; }
        public int Time { set; get; }

        public Skill() { }
        public Skill(string name , string difficult, string specification, List<Skill> requirement, int time)
        {
            Name = name;
            Difficult = difficult;
            Specification = specification;
            Requirement = requirement;
            Time = time;
        }
        public override string ToString()
        {
            return $"Name = \"{Name}\" Difficult = \"{Difficult}\" Specification = \"{Specification}\"" +
                $" Requirement = \"{Requirement}\" Time = \"{Time}\"";
        }
    }
}
