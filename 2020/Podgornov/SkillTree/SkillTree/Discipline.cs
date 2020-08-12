namespace SkillTree
{
    public class Discipline
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public readonly int Id;

        public Discipline(int id, string name, string description)
        {
            Name = name;
            Description = description;
            Id = id;
        }

        public override string ToString()
        {
            return $"{Id} Name:\"{Name}\"\nDescription:\"{Description}\"";
        }
    }
}
