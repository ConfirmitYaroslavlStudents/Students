using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SkillTree
{
    [JsonObject]
    public class Graph<T>:IEnumerable<Vertex<T>>
    {
        private int _maxId;

        public readonly Dictionary<int, Vertex<T>> Vertexes;

        public int MaxId
        {
            get => _maxId;
            set
            {
                if(_maxId == 0)
                    AddLinksToVertexes();
                _maxId = value;
            }
        }

        public Graph()
        {
            Vertexes = new Dictionary<int, Vertex<T>>();
        }        

        public Vertex<T> this[int id] => Vertexes[id];

        public void AddVertex(T value)
        {
            var vertex = new Vertex<T>(value, MaxId, Vertexes);
            Vertexes.Add(MaxId, vertex);
            MaxId++;
        }

        public void RemoveVertex(int id)
        {
            var vertex = Vertexes[id];          
            vertex.DeletingAllLinks();
            Vertexes.Remove(id);
        }

        public void AddDependence(int beginId, int endId) => Vertexes[endId].AddDependence(Vertexes[beginId]);

        public void RemoveDependence(int beginId, int endId) => Vertexes[endId].RemoveDependence(Vertexes[beginId]);

        public IEnumerator<Vertex<T>> GetEnumerator() => Vertexes.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void AddLinksToVertexes()
        {
            foreach (var vertex in Vertexes.Values)
            {
                vertex.Vertexes = this.Vertexes;
            }
        }
    }
}
