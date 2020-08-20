using System.Collections.Generic;

namespace SkillTree.Graph
{
    public class Vertex
    {
        public Vertex() { }
        public Vertex(Skill skill)
        {
            Skill = skill;
            Edges = new List<Edge>();
        }     

        public Skill Skill { get; set; }
        public List<Edge> Edges { get; set; }

        public void AddEdge(Vertex vertex)
        {
            Edges.Add(new Edge(vertex));
        }
    }
}
