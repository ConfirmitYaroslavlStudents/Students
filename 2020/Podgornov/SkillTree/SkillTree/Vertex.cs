using System;
using System.Collections.Generic;

namespace SkillTree
{
    public class Vertex<T>
    {
        public List<int> Dependencies { get; set; }

        public Vertex(T value, int id)
        {
            Value = value;
            Id = id;
            Dependencies = new List<int>();
        }

        public int Id { get; set; }

        public T Value { get; set; }

        internal void AddDependence(Vertex<T> vertex)
        {
            if (Dependencies.Contains(vertex.Id))
                throw new GraphException("There was dependency.");
            if (vertex.Id == Id)
                throw new GraphException("You can't add a dependency on yourself.");
            Dependencies.Add(vertex.Id);
        }

        internal void RemoveDependence(Vertex<T> vertex)
        {
            if (!Dependencies.Remove(vertex.Id))
                throw new InvalidOperationException("There was no dependency.");
        }

        public override string ToString()
        {
            return $"{nameof(T)} id: {Id}";
        }
    }
}