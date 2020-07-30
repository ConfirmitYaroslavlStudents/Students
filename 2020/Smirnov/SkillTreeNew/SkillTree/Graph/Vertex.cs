using System.Collections.Generic;

namespace SkillTree.Graph
{
    public class Vertex
    {
        public Vertex(Skill skill)
        {
            Skill = skill;
            Edges = new List<Edge>();
        }

        public Skill Skill { get; }
        public List<Edge> Edges { get; }

        public void AddEdge(Vertex vertex)
        {
            Edges.Add(new Edge(vertex));
        }
    }
}
