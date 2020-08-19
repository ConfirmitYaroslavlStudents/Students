namespace SkillTree
{
    public class Skill
    {        
        public SkillComplexity Complexity { get; set; }

        public string Description { get; set; }

        public int TrainingTime { get; set; }

        public Skill() { }

        public Skill(string description, int trainingTime, SkillComplexity complexity)
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
