using System;
using System.Collections.Generic;

namespace SkillTree.Graph
{
    public class Graph
    {       
        public Graph()
        {
            Vertices = new List<Vertex>();
        }
        public List<Vertex> Vertices { get; }

        public int Count 
        {
            get 
            {
                return Vertices.Count;
            }
        }
        public void AddVertex(Skill skill)
        {
            if (FindVertex(skill.Name) != null)
            {
                throw new InvalidOperationException("This skill already exists");
            }
            Vertices.Add(new Vertex(skill));
        }
        public void AddEdge(string firstName, string secondName)
        {
            if (firstName == secondName)
            {
                throw new InvalidOperationException("Names are equal");
            }
            var firstVertex = FindVertex(firstName);
            if (firstVertex == null)
            {
                throw new InvalidOperationException($"{firstName} not found");
            }
            var secondVertex = FindVertex(secondName);
            if (secondVertex == null)
            {
                throw new InvalidOperationException($"{secondName} not found");
            }
            firstVertex.AddEdge(secondVertex);
        }
        public Vertex FindVertex(string vertexName)
        {
            foreach (var vertex in Vertices)
            {
                if (vertex.Skill.Name.Equals(vertexName))
                {
                    return vertex;
                }
            }
            return null;
        }  
        public List<Vertex> ReturnAllVertices()
        {
            return Vertices;
        }
    }
}
