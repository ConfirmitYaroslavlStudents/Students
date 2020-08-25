using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SkillTree.Graph
{
    public class Vertex: IEquatable<Vertex>
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

        public bool Equals( Vertex other)
        {
            return this.Skill.Name.Equals(other.Skill.Name);
        }
    }
}
