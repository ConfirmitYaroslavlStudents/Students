namespace SkillTree
{
    public class Discipline
    {
        public string Name { get; set; }

        public Discipline(string name)
        {
            Name = name;
        }

        public override string ToString() => $"Name:\"{Name}\"";
    }
}
