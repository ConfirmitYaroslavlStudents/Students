using System;
using System.Collections.Generic;
using System.Linq;

namespace SkillTree
{
    public class Vertex<T>
    {
        internal Dictionary<int, Vertex<T>> Vertexes;

        public List<int> Dependencies { get; set; }

        private bool _finished;

        private bool _available;

        public Vertex(T value, int id, Dictionary<int, Vertex<T>> vertexes, bool available = true)
        {
            Value = value;
            Id = id;
            Vertexes = vertexes;
            Dependencies = new List<int>();
            _available = available;
        }

        public int Id { get; set; }

        public T Value { get; set; }

        public bool Finished
        {
            get => _finished;
            set
            {
                if (_finished == value) return;
                if (value)
                {
                    if (_available)
                        _finished = true;
                    else
                        throw new ArgumentException("Vertex not available.");
                }
                else
                    throw new ArgumentException("Vertex is finished.");
            }
        }

        public bool Available
        {
            get => _available;
            set
            {
                if (_available == value) return;
                var flag = FinishForAllDependencies();
                if ((!flag || value) && (flag || !value))
                {
                    _available = value;
                }
                else
                {
                    var argumentException = value
                        ? new ArgumentException("Vertex isn't available.")
                        : new ArgumentException("Vertex is available.");
                    throw argumentException;
                }
            }
        }

        public void Finish()
        {
            Finished = true;
            foreach (var vertex in Vertexes.Values.Where(vertex => vertex.Dependencies.Contains(Id)))
            {
                if (vertex.FinishForAllDependencies())
                {
                    vertex._available = true;
                }
            }
        }

        internal void AddDependence(Vertex<T> vertex)
        {
            if (Dependencies.Contains(vertex.Id))
                throw new GraphException("There was dependency.");
            if (vertex.Id == Id)
                throw new GraphException("You can't add a dependency on yourself.");
            if (CycleWasFormed(vertex, this))
            {
                throw new GraphException("Cycle was formed.");
            }
            Dependencies.Add(vertex.Id);
            _available = false;
        }

        internal void RemoveDependence(Vertex<T> vertex)
        {
            if (!Dependencies.Remove(vertex.Id))
                throw new InvalidOperationException("There was no dependency.");
            if (Dependencies.Count == 0) _available = true;
        }

        internal void DeletingAllLinks()
        {
            foreach (var vertex in Vertexes.Values.Where(vertex => vertex.Dependencies.Contains(this.Id)))
            {
                vertex.Dependencies.Remove(Id);
                if (vertex.FinishForAllDependencies())
                {
                    vertex._available = true;
                }
            }
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
                    stack.Push(Vertexes[id]);
                }
            }
            return false;
        }

        private bool FinishForAllDependencies() =>
            (from id in Dependencies select Vertexes[id]).All(i => i.Finished);

        public override string ToString()
        {
            return $"Element {Id}";
        }

        public IEnumerable<Vertex<T>> GetAllDependencies()
        {
            var dictionary = new Dictionary<int, Vertex<T>>();
            var stack = new Stack<Duplex<T>>();
            stack.Push(new Duplex<T>(this));
            while (stack.Count != 0) 
            {
                if (stack.Peek().IsMatch)
                {
                    var currentVertex = stack.Pop().Vertex;
                    if(dictionary.ContainsKey(currentVertex.Id))
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
                    foreach (var vertex in stack.Peek().Vertex.Dependencies.Select(id => Vertexes[id]))
                    {
                        stack.Push(new Duplex<T>(vertex));
                    }
                }
            }

            dictionary.Remove(this.Id);
            return dictionary.Values;
        }
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