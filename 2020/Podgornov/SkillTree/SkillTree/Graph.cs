using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SkillTree
{
    [JsonObject]
    public class Graph<T>:IEnumerable<Vertex<T>>
    {
        private Dictionary<int, Vertex<T>> _vertexesDictionary;

        [JsonIgnore]
        public int MaxId
        {
            get;
            private set;
        }

        public Graph()
        {
            _vertexesDictionary = new Dictionary<int, Vertex<T>>();
        }

        public Graph(IEnumerable<Vertex<T>> vertexes)
        {
            _vertexesDictionary = new Dictionary<int, Vertex<T>>();
            var vertices = vertexes as Vertex<T>[] ?? vertexes.ToArray();
            foreach (var vertex in vertices)
            {
                _vertexesDictionary.Add(vertex.Id, vertex);
            }

            if (vertices.Length == 0)
                MaxId = 0;
            else
                MaxId = _vertexesDictionary.Values.Select(i => i.Id).Max() + 1;
        }

        public Vertex<T> this[int id] => _vertexesDictionary[id];

        public Vertex<T> GetVertexById(int id) => _vertexesDictionary[id];

        public Vertex<T> AddVertex(T value)
        {
            var vertex = new Vertex<T>(value, MaxId);
            _vertexesDictionary.Add(MaxId, vertex);
            MaxId++;
            return vertex;
        }

        public void RemoveVertex(int id)
        {
            foreach (var vertex in _vertexesDictionary.Values.Where(v => v.Dependencies.Contains(id)))
            {
                vertex.Dependencies.Remove(id);
            }
            _vertexesDictionary.Remove(id);
        }

        public void AddDependence(Vertex<T> beginVertex, Vertex<T> endVertex)
        {
            endVertex.AddDependence(beginVertex);
            if (CycleWasFormed(beginVertex, endVertex))
                throw new GraphException("Cycle was formed.");
        }

        private bool CycleWasFormed(Vertex<T> start, Vertex<T> end)
        {
            var stack = new Stack<Vertex<T>>();
            stack.Push(start);
            while (stack.Count != 0)
            {
                if (stack.Peek().Dependencies.Count == 0)
                {
                    stack.Pop();
                    continue;
                }
                foreach (var id in stack.Pop().Dependencies)
                {
                    if (id == end.Id) return true;
                    stack.Push(_vertexesDictionary[id]);
                }
            }
            return false;
        }

        public void RemoveDependence(Vertex<T> beginVertex, Vertex<T> endVertex)
        {
            endVertex.RemoveDependence(beginVertex);
        }

        public IEnumerable<Vertex<T>> GetAllDependencies(Vertex<T> vertex)
        {
            var dictionary = new Dictionary<int, Vertex<T>>();
            var stack = new Stack<Duplex<T>>();
            stack.Push(new Duplex<T>(vertex));
            while (stack.Count != 0)
            {
                if (stack.Peek().IsMatch)
                {
                    var currentVertex = stack.Pop().Vertex;
                    if (dictionary.ContainsKey(currentVertex.Id))
                        continue;
                    dictionary.Add(currentVertex.Id, currentVertex);
                }
                else if (dictionary.ContainsKey(stack.Peek().Vertex.Id))
                {
                    stack.Pop();
                }
                else
                {
                    stack.Peek().IsMatch = true;
                    foreach (var currentVertex in stack.Peek().Vertex.Dependencies.Select(id => _vertexesDictionary[id]))
                    {
                        stack.Push(new Duplex<T>(currentVertex));
                    }
                }
            }

            dictionary.Remove(vertex.Id);
            return dictionary.Values;
        }

        public IEnumerator<Vertex<T>> GetEnumerator() => _vertexesDictionary.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Duplex<T>
    {
        public bool IsMatch { get; set; }

        public Vertex<T> Vertex { get; set; }

        public Duplex(Vertex<T> vertex)
        {
            Vertex = vertex;
            IsMatch = false;
        }
    }
}
