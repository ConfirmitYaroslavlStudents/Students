using System;
using System.Xml.Serialization;

namespace SkillTree
{
    [Serializable]      
    public class Skill
    {        
        public Skill_Complexity Complexity { get; set; }

        public string Description { get; set; }

        public string TrainingTime { get; set; }

        public Skill() { }

        public Skill(string Description, string TrainingTime, Skill_Complexity Complexity)
        {
            this.Description = Description;
            this.TrainingTime = TrainingTime;
            this.Complexity = Complexity;
        }        
    }
}
