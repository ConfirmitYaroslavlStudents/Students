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
        public List<Vertex> Vertices { get; set; }

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
        public List<Vertex> BreadthFirstSearch(Vertex start)
        {
            var queue = new Queue<Vertex>();
            var path = new List<Vertex>();
            var visited = new Dictionary<string, bool>();

            queue.Enqueue(start);
            path.Add(start);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                foreach (var edge in FindVertex(vertex.Skill.Name).Edges)
                {
                    if (!visited.ContainsKey(edge.ConnectedVertex.Skill.Name))
                    {
                        visited.Add(edge.ConnectedVertex.Skill.Name, false);
                    }
                    if (!visited[edge.ConnectedVertex.Skill.Name])
                    {
                        queue.Enqueue(edge.ConnectedVertex);
                        path.Add(edge.ConnectedVertex);
                        visited[edge.ConnectedVertex.Skill.Name] = true;
                    }
                }
            }

            return path;
        }
        public List<Vertex> ReturnAllVertices()
        {
            return Vertices;
        }
    }
}
