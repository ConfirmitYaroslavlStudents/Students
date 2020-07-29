using System.Collections;
using System.Collections.Generic;

namespace SkillTree
{ 
    public class Graph:IEnumerable<Vertex>
    {             
        private readonly Dictionary<int, Vertex> _vertexes;

        public int MaxId { get; private set; }

        public Graph()
        {
            _vertexes = new Dictionary<int, Vertex>();
        }        

        public Vertex this[int id] => _vertexes[id];

        public void AddVertex(Skill skill)
        {
            var vertex = new Vertex(skill, MaxId);
            _vertexes.Add(MaxId, vertex);
            MaxId++;
        }

        internal void AddVertexes(Vertex[] vertexes)
        {
            foreach (var vertex in vertexes)
            {
                _vertexes.Add(MaxId, vertex);
                MaxId++;
            }
        }

        public void RemoveVertex(int id)
        {
            var vertex = _vertexes[id];          
            vertex.DeletingAllLinks();
            _vertexes.Remove(id);
        }

        public void AddDependence(int beginId, int endId) => _vertexes[endId].AddDependence(_vertexes[beginId]);

        public void RemoveDependence(int beginId, int endId) => _vertexes[endId].RemoveDependence(_vertexes[beginId]);

        public IEnumerator<Vertex> GetEnumerator() => _vertexes.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public GraphInformation GetGraphInformation() => new GraphInformation(_vertexes);
    }
}
