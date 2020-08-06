using System.Diagnostics;

namespace SkillTree.Graph
{
    [DebuggerDisplay("Name = {Name} Difficult = {Difficult} Specification = {Specification} Time = {Time}")]
    public class Skill
    {
        public Skill(string name, string difficulty, string specification,  int time)
        {
            Name = name;
            Difficult = difficulty;
            Specification = specification;          
            Time = time;
        }

        public string Name { get; }
        public string Difficult { get; }
        public string Specification { get; }
        public int Time { get; }
    }
}
