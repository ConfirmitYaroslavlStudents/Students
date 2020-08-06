namespace SkillTree.Classes
{
    public abstract class Person
    {
        public string Name {  get; set; }

        public Person() { }
        public Person(string name)
        {
            Name = name;
        }

    }
}
