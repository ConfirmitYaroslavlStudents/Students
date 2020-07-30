using System.Collections.Generic;
using SkillTree.Graph;

namespace User
{
    public class User
    {
        public User(string name)
        {
            Name = name;
            LearnedSkills = new List<Skill>();
            LearnedDisciplines = new Dictionary<string, Graph>();
        }
        public string Name { get; }
        public List<Skill> LearnedSkills { get; }
        public Dictionary<string, Graph> LearnedDisciplines { get; }
    }
}
