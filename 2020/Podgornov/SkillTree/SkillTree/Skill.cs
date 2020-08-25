using System;
using System.Linq;

namespace SkillTree
{
    public class Skill
    {
        private int _trainingTime;

        public SkillComplexity Complexity { get; set; }

        public string Description { get; set; }

        public int TrainingTime
        {
            get => _trainingTime;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Incorrect training time.");
                _trainingTime = value;
            }
        }

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

    public static class SkillTreeExtension
    {
        public static int GetDisciplineTanningTime(this Graph<Skill> graph,Vertex<Skill> vertex)
        {
            return graph.GetAllDependencies(vertex).Select(s => s.Value.TrainingTime).Sum();
        } 
    }
}
