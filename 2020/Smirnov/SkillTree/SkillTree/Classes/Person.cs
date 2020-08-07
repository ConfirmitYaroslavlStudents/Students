namespace SkillTree.Classes
{
    public abstract class Person
    {
        public string Name { set; get; }

        public Person() { }
        public Person(string name)
        {
            Name = name;
        }

    }
}
