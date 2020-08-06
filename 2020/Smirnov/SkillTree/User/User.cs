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
            LearnedDisciplines = new List<string>();
        }
        public User(string name, List<Skill> learnedSkills, List<string> learnedDisciplines)
        {
            Name = name;
            LearnedSkills = learnedSkills;
            LearnedDisciplines = learnedDisciplines;
        }
        public string Name { get; }
        public List<Skill> LearnedSkills { get; }
        public List<string> LearnedDisciplines { get; }
    }
}
