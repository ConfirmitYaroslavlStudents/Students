namespace SkillTree
{
    public class Skill
    {        
        public SkillComplexity Complexity { get; set; }

        public string Description { get; set; }

        public string TrainingTime { get; set; }

        public Skill() { }

        public Skill(string description, string trainingTime, SkillComplexity complexity)
        {
            this.Description = description;
            this.TrainingTime = trainingTime;
            this.Complexity = complexity;
        }

        public override string ToString()
        {
            return $"Description:\n{Description}\nTraining Time:\n{TrainingTime}\nComplexity:\n{Complexity.ToString()}";
        }
    }
}
