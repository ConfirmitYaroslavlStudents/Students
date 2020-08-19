using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;

namespace SkillTree
{
    [JsonObject]
    public class Graph<T>:IEnumerable<Vertex<T>>
    {
        private Dictionary<int, Vertex<T>> _vertexesDictionary;

        public ReadOnlyDictionary<int, Vertex<T>> VertexesDictionary => new ReadOnlyDictionary<int, Vertex<T>>(this._vertexesDictionary);

        public int MaxId
        {
            get;
            private set;
        }

        public Graph()
        {
            _vertexesDictionary = new Dictionary<int, Vertex<T>>();
        }

        public Graph(IDictionary<int,Vertex<T>> vertexesDictionary)
        {
            _vertexesDictionary = new Dictionary<int, Vertex<T>>(vertexesDictionary);
            MaxId = _vertexesDictionary.Values.Select(i => i.Id).Max() + 1;
            AddLinksToVertexes();
        }

        public Vertex<T> this[int id] => _vertexesDictionary[id];

        public void AddVertex(T value)
        {
            var vertex = new Vertex<T>(value, MaxId, _vertexesDictionary);
            _vertexesDictionary.Add(MaxId, vertex);
            MaxId++;
        }

        public void RemoveVertex(int id)
        {
            var vertex = _vertexesDictionary[id];          
            vertex.DeletingAllLinks();
            _vertexesDictionary.Remove(id);
        }

        public void AddDependence(int beginId, int endId) => _vertexesDictionary[endId].AddDependence(_vertexesDictionary[beginId]);

        public void RemoveDependence(int beginId, int endId) => _vertexesDictionary[endId].RemoveDependence(_vertexesDictionary[beginId]);

        public IEnumerator<Vertex<T>> GetEnumerator() => _vertexesDictionary.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void AddLinksToVertexes()
        {
            foreach (var vertex in _vertexesDictionary.Values)
            {
                vertex.Vertexes = this._vertexesDictionary;
            }
        }
    }
}
