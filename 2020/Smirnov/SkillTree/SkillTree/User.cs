using System.Collections.Generic;
using SkillTree.Graph;

namespace SkillTree
{
    public class User
    {
        public User(string name)
        {
            Name = name;
            LearnedSkills = new List<Skill>();
            LearnedDisciplines = new List<string>();
        }
        public User(string name, List<Skill> learnedSkills, List<string> learnedDisciplines)
        {
            Name = name;
            LearnedSkills = learnedSkills;
            LearnedDisciplines = learnedDisciplines;
        }
        public User() { }
        public string Name { get; set;  }
        public List<Skill> LearnedSkills { get; set;  }
        public List<string> LearnedDisciplines { get; set; }
    }
}
