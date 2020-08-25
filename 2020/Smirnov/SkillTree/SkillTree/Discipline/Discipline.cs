namespace SkillTree
{
    public class Discipline
    {
        public string Name { get; }
        public Graph.Graph Graph { get; }
        public Discipline(string name)
        {
            Name = name;
            Graph = new Graph.Graph();
        }
        public Discipline()
        {
        }
        public Discipline(string name, Graph.Graph graph)
        {
            Name = name;
            Graph = graph;
        }
    }
}
