using System.Collections.Generic;
using System;
namespace SkillTree.Classes
{
    [Serializable]
    public class Skill : IEquatable<Skill>
    {
        public string Name { get; set; }
        public string Difficult { get; set; }
        public string Specification { get; set; }
        public List<Requirement> Requirements { get; set; }
        public int Time { get; set; }

        public Skill() { }
        public Skill(string name , string difficult, string specification, int time)
        {
            Name = name;
            Difficult = difficult;
            Specification = specification;
            Requirements = new List<Requirement>();
            Time = time;
        }
        public override string ToString()
        {
            return $"Name = \"{Name}\" Difficult = \"{Difficult}\" Specification = \"{Specification}\"" +
                $" Requirement = \"{ReturnNameRequirements()}\" Time = \"{Time}\"";
        }
        public void AddRequirement(Skill skill)
        {
            Requirements.Add(new Requirement(skill));

        }
        public string ReturnNameRequirements()
        {
            string toString = "";
            foreach (var requirement in Requirements)
            {
                if (toString == "")
                {
                    toString = requirement.ConnectedVertex.Name;
                }
                else
                {
                    toString = toString + " " + requirement.ConnectedVertex.Name;
                }
            }
            return toString;
        }
        public bool Equals(Skill other)
        {
            if (other == null)
                return false;

            if (other.Name == this.Name && other.Difficult == this.Difficult &&
                other.ReturnNameRequirements() == this.ReturnNameRequirements() &&
                other.Specification == this.Specification && other.Time == this.Time)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
