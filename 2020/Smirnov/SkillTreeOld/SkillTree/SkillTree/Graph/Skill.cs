using System.Diagnostics;

namespace SkillTree.Graph
{
    [DebuggerDisplay("Name = {Name} Difficult = {Difficult} Specification = {Specification} Time = {Time}")]
    public class Skill
    {
        public Skill() { }
        public Skill(string name, string difficulty, string description,  int time)
        {
            Name = name;
            Difficult = difficulty;
            Description = description;          
            Time = time;
        }

        public string Name { get; set; }
        public string Difficult { get; set; }
        public string Description { get; set; }
        public int Time { get; set; }
    }
}
