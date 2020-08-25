using System;
using System.Collections.Generic;
using System.Linq;

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
            if (Contains(skill.Name))
            {
                throw new InvalidOperationException("This skill already exists");
            }
            Vertices.Add(new Vertex(skill));
        }
        public void AddEdge(string firstName, string secondName)
        {
            if (firstName.Equals(secondName, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new InvalidOperationException("Names are equal");
            }
            var firstVertex = TryGetVertex(firstName);
            var secondVertex = TryGetVertex(secondName);
            firstVertex.AddEdge(secondVertex);
        }
        public Vertex TryGetVertex(string vertexName)
        {
            foreach (var vertex in Vertices)
            {
                if (vertex.Skill.Name.Equals(vertexName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return vertex;
                }
            }
            throw new InvalidOperationException($"{vertexName} not found");
        }
        public bool Contains(string vertexName)
        {
            foreach (var vertex in Vertices)
            {
                if (vertex.Skill.Name.Equals(vertexName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
        public List<Vertex> SearchPath(Vertex start)
        {
            var queue = new Queue<Vertex>();
            var path = new List<Vertex>();
            var visited = new Dictionary<string, bool>();

            queue.Enqueue(start);
            path.Add(start);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                foreach (var edge in TryGetVertex(vertex.Skill.Name).Edges)
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
                    else
                    {
                        path.Remove(edge.ConnectedVertex);
                        queue.Enqueue(edge.ConnectedVertex);
                        path.Add(edge.ConnectedVertex);
                    }
                }
            }
            path.Distinct();

            return path;
        }
        public List<Vertex> ReturnAllVertices()
        {
            return Vertices;
        }
    }
}
